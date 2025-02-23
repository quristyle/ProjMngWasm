using System;
using ProjMngWasm.Models;

namespace ProjMngWasm.Services;


public interface IUMSService {
        Task<IEnumerable<University>> GetUniversities(string token);
        Task<IEnumerable<University>> GetData(string url, string token);
    }

public class UMSService : BaseService, IUMSService {
        public UMSService(HttpClient httpClient) : base(httpClient) { }

        public async Task<IEnumerable<University>> GetUniversities(string token) {

Req req = new Req(){
QueryName="SP_MAIN_MENU_SELECT"
, ReturnQueryParameter=false
, QueryParameters= new List<QueryParameter>(){
    new QueryParameter(){
        ParameterName="USER_ID"
        , ParameterValue="test"
        , ParameterDirection=1
        , Prefix="IN_"
    }
}
};
await PostMethodRes("api/QueryService/ExecuteRequestAsync", req, token);

return null;
            //return await GetMethodList<University>($"");
        }


        public async Task<IEnumerable<University>> GetData(string url, string token) {

Req req = new Req(){
QueryName="SP_USER_LOGIN_SELECT"
, QueryParameters= new List<QueryParameter>(){
    new QueryParameter(){ ParameterName="USER_ID" , ParameterValue="test" }
    , new QueryParameter(){ ParameterName="PASSWORD" , ParameterValue="test" }
    , new QueryParameter(){ ParameterName="IS_HAN" , ParameterValue="H" }
}
};



await PostMethodRes("api/Account/Login", req, token);

return null;
            //return await GetMethodList<University>($"");
        }



    }


    public class Req{
    public string QueryName{get;set;}
     public List<QueryParameter> QueryParameters{get;set;} = new List<QueryParameter>();
     public bool ReturnQueryParameter{get;set;}
    }

    public class QueryParameter{
     public string ParameterName{get;set;}
     public string ParameterValue{get;set;}
     public int ParameterDirection{get;set;} = 1;
     public string Prefix{get;set;} = "IN_";
    }

