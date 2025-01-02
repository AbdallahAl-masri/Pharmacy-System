document.addEventListener("DOMContentLoaded", function () {
    // Initialize the default field
    initializeMedicineSearch($(".medicine-search"));

    document.getElementById("addDetail").addEventListener("click", function () {
        const rowCount = $("#detailsTable tbody tr").length;
        const newRow = `
            <tr>
                <td>
                    <div class="medicine-search-wrapper">
                        <input
                            type="text"
                            class="form-control medicine-search"
                            name="InvoiceDetails[${rowCount}].MedicineName"
                            placeholder="Search for medicine..."
                        />
                        <ul class="medicine-list" style="display: none; position: absolute; z-index: 1000;"></ul>
                    </div>
                </td>
                <td><input name="InvoiceDetails[${rowCount}].Quantity" type="number" class="form-control Qty" /></td>
                <td><input name="InvoiceDetails[${rowCount}].Price" type="text" class="form-control price" readonly /></td>
                <td><input name="InvoiceDetails[${rowCount}].TotalPrice" type="number" class="form-control total-price" readonly /></td>
                <td><button type="button" class="removeDetail btn rounded-pill btn-danger">Remove</button></td>
            </tr>`;
        $("#detailsTable tbody").append(newRow);

        initializeMedicineSearch($("#detailsTable tbody tr:last .medicine-search"));
        attachCalculationHandlers($("#detailsTable tbody tr:last"));
    });

    function initializeMedicineSearch($input) {
        $input.on("input", function () {
            const query = $(this).val().trim();
            const $list = $(this).siblings(".medicine-list");
            if (query.length === 0) {
                $list.hide().empty();
                return;
            }
            $.ajax({
                url: '/Invoice/SearchMedicines',
                type: 'GET',
                data: { query },
                success: function (data) {
                    $list.empty();
                    if (data.length === 0) {
                        $list.append('<li class="no-result">No medicines found</li>');
                    } else {
                        data.forEach(function (med) {
                            $list.append(`
                                <li class="medicine-item"
                                    data-id="${med.medicineId}"
                                    data-name="${med.medicineName}"
                                    data-price="${med.medicinePrice}"
                                    data-quantity="${med.medicineQTY}"
                                    data-discount="${med.discount}">
                                    ${med.medicineName}
                                </li>`);
                        });
                    }
                    $list.show();
                },
                error: function () {
                    $list.empty().append('<li class="no-result">Error fetching medicines. Try again later.</li>').show();
                }
            });
        });

        $input.siblings(".medicine-list").on("click", ".medicine-item", function () {
            const $item = $(this);
            const medicineName = $item.data("name");
            const medicineId = $item.data("id");
            const medicinePrice = $item.data("price");
            const medicineQty = $item.data("quantity");
            const medicineDiscount = $item.data("discount");

            $input.val(medicineName);
            $(`<input type="hidden" name="InvoiceDetails[${medicineId}].MedicineId" value="${medicineId}" />`).appendTo($input.closest("td"));

            $input.closest("tr").find(".price").val(medicinePrice);
            $("#Discount").val(medicineDiscount);
            $input.closest("tr").data("availableQty", medicineQty);
            $item.parent().hide().empty();
        });
    }

    function attachCalculationHandlers($row) {
        $row.find(".Qty, .price").on("input", function () {
            calculateRowTotal($row);
        });
    }

    function calculateRowTotal($row) {
        const qty = parseFloat($row.find(".Qty").val()) || 0;
        const price = parseFloat($row.find(".price").val()) || 0;
        const availableQty = parseFloat($row.data("availableQty"));
        if (qty > availableQty) {
            alert(`Entered quantity exceeds available (${availableQty}).`);
            $row.find(".Qty").val(availableQty);
            return;
        }
        const total = qty * price;
        $row.find(".total-price").val(total.toFixed(2));
        calculateGrandTotal();
    }

    function calculateGrandTotal() {
        let grandTotal = 0;
        $(".total-price").each(function () {
            grandTotal += parseFloat($(this).val()) || 0;
        });
        $("#grandTotal").val(grandTotal.toFixed(2));
    }

    document.addEventListener("click", function (e) {
        if (e.target && e.target.classList.contains("removeDetail")) {
            const rowCount = $("#detailsTable tbody tr").length;
            if (rowCount === 1) {
                $(e.target).closest("tr").find("input").val('');
            } else {
                $(e.target).closest("tr").remove();
            }
            calculateGrandTotal();
        }
    });
});
