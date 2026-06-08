using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingCenter.App.DAO
{
    public class GPLX_CDB_CSDT_DAO
    {
        private static GPLX_CDB_CSDT_DAO instance;

        public static GPLX_CDB_CSDT_DAO Instance
        {
            get { if (instance == null) instance = new GPLX_CDB_CSDT_DAO(); return GPLX_CDB_CSDT_DAO.instance; }
            private set => GPLX_CDB_CSDT_DAO.instance = value;
        }

        public GPLX_CDB_CSDT_DAO()
        {
        }


    }
}
