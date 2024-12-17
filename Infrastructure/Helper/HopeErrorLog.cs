using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helper
{
     public class HopeErrorLog
    {
        public void AddErrorLog(Exception ex, string id)
        {
            try
            {
                EntitiyComponent.DBEntities.ErrorLog obj = new EntitiyComponent.DBEntities.ErrorLog();
                obj.ErrorExeption = ex.InnerException != null ? ex.InnerException.ToString() : "";
                obj.ErrorMessage = ex.Message != null ? ex.Message.ToString() : "";
                obj.ModuleName = "User - JobGetAllDescriptions ";
                obj.TransactionDate = DateTime.Now;
            }
            catch (Exception ex1 )
            {

                var applog = new EventLog("Application");
                applog.Source = "Application ";
                applog.WriteEntry(ex1.Message, EventLogEntryType.Error);
            }
           


        }

        public void AddErrorLog(Exception ex, string id, string v)
        {
            throw new NotImplementedException();
        }
    }
}
