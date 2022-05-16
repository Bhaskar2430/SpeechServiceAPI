using JustSpeak.Common;
using JustSpeak.Interfaces;
using JustSpeak.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationBlocks.Data;
namespace JustSpeak.Servicehelper
{
    public class ServiceHelper:IServiceHelper
    {
        private readonly IConstants _constants;
        private readonly string eConstring;

        public ServiceHelper(IConstants constants)
        {
            _constants = constants;
            eConstring = _constants.EConnectionString();
        }


        public List<EmpLegalNames> GetEmpLegalNames()
        {
            List<EmpLegalNames> legalNameslst = new List<EmpLegalNames>();
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlDataReader dr = SqlHelper.ExecuteReader(eConstring, CommandType.StoredProcedure, "[GetEmpLegalNames]", parameters.ToArray());
            while (dr.Read())
            {
                EmpLegalNames eln = new EmpLegalNames();
                eln.LegalNameId = dr["LegalNameId"] != DBNull.Value ? Convert.ToInt32(dr["LegalNameId"]) : 0;
                eln.LegalNames = dr["LegalNames"] != DBNull.Value ? Convert.ToString(dr["LegalNames"]) : string.Empty;

                legalNameslst.Add(eln);


            }

            return legalNameslst;
        }

        //public List<StandardModelVM> GetStandardModelVMs()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
