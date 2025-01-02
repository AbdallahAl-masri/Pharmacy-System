$('#DepartmentList').on('change', function () {
    $.ajax({
        url: '/User/GetAllSectionByDeptId',
        type: 'GET',
        data: { DeptId: $('#DepartmentList').val() },
        success: function (Result) {
            $('#SectionList option').remove();
            $('#SectionList').append("<option value=''>Please Select</option>");
            $.each(Result, function () {
                $('#SectionList').append('<option value=' + this.sectionId + '>' +
                    this.sectionName + '</option>')
            });
        }
    });
});