using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrivingCenter.App.DAO
{
    public class DAO
    {
        private static DAO instance;

        private static readonly object lockObj = new object();

        private DAO() { }

        public static DAO Instance
        {
            get
            {
                lock (lockObj)
                {
                    if (instance == null)
                        instance = new DAO();
                    return instance;
                }
            }
        }

        public void SetInstance(object obj)
        {
            string query = "SELECT * FROM [I2ECABIN3D].[dbo].[GiaoVien]";
            DataProvider.DataProvider.Instance.ExecuteQuery(query);
        }
    }
}
