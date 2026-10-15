using DrivingCenter.App.Common;
using DrivingCenter.App.Enums;
using System.Collections.Generic;

namespace DrivingCenter.App.DataProvider
{
    public static class DbConnectionFactory
    {
        private static readonly Dictionary<DatabaseType, string> _connections =
        new Dictionary<DatabaseType, string>()
        {
            { DatabaseType.db_Gplx, AppConstants.connectionStringGplx },
            { DatabaseType.db_DAT , AppConstants.connectionStringDAT },
            { DatabaseType.db_DatImg, AppConstants.connectionStringDatImg },
            { DatabaseType.db_CaBinEMC, AppConstants.connectionStringCaBinEMC },
            { DatabaseType.db_CabinEco, AppConstants.connectionStringCabinEco },
            { DatabaseType.db_GplxCDB, AppConstants.connectionStringGplx_CDB },
            { DatabaseType.db_BeeSolution, AppConstants.connectionStringApp }
        };

        public static string GetConnection(DatabaseType db)
        {
            return _connections[db];
        }
    }
}
