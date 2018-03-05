using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;
using System.Data;
using System.Collections.Generic;
using VCSWEB1.Models;

namespace VCSWEB1.Controllers
{
    public class ResponseController : ApiController
    {

        private const string STORED_PROCEUDRE = "sp";
        private const string COMMAND = "cmd";
        private const string QUERY = "query";
        
        public IHttpActionResult GetResponse() {

            Response response = new Response();

            try
            {
                string stored_procedure = "";
                string command = "";
                string query = "";
                Dictionary<string, object> param = new Dictionary<string, object>();

                IEnumerable<KeyValuePair<string, string>> QueryValues = Request.GetQueryNameValuePairs();
                foreach (KeyValuePair<string, string> keyvalue in QueryValues)
                {
                    if      (keyvalue.Key == STORED_PROCEUDRE)   stored_procedure = keyvalue.Value;
                    else if (keyvalue.Key == COMMAND)            command = keyvalue.Value;
                    else if (keyvalue.Key == QUERY)              query = keyvalue.Value;
                    else {
                        param.Add("@" + keyvalue.Key, keyvalue.Value);
                    }
                }

                if (stored_procedure != "")
                {
                    DataTable dt = DBConn.ExecStoredProcedure(stored_procedure, param);
                    response.build_data(dt);
                    response.type = "success";
                    response.status = "";
                    return Ok(response);
                }
                else if (command != "")
                {
                    // ignored, if stored_procedure is set
                    int rows_affected = DBConn.ExecNonQuery(command);
                    response.type = "success";
                    response.status = rows_affected.ToString()+" rows affected";
                    return Ok(response);
                }
                else if (query != "")
                {
                    //ignored, if either stored_procedure or command is set.
                    DataTable dt = DBConn.ExecQuery(query);
                    response.build_data(dt);
                    response.type = "success";
                    response.status = "";
                    return Ok(response);
                }
                else {
                    response.type = "fail";
                    response.status = "SP|CMD|QUERY Not defined";
                    return Ok(response);
                }

            }
            catch (Exception ex) {
                // Error, returned from SP|CMD|QUERY
                response.type = "fail";
                response.status = ex.Message;
                return Ok(response);
            }
        }
    }
}
