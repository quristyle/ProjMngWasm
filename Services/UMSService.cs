using System;
using System.Data;
using ProjMngWasm.Models;

namespace ProjMngWasm.Services;



public class UMSService : BaseService, IUMSService {
  public UMSService(HttpClient httpClient, ISessionStorageService sess) : base(httpClient, sess) { }

  public async Task<string> GetData(string path, Dictionary<string, object> dic) {

    /*
    Req req = CreateReq("SP_MAIN_MENU_SELECT", new Dictionary<string, object>() {
      { "USER_ID","test" }
    });
    */

    Req req = CreateReq(path, dic);

    return await PostMethodObj("api/QueryService/ExecuteRequestAsync", req);

    //return null;
    //return await GetMethodList<University>($"");
  }



  public async Task Login() {

    Req req = CreateReq("SP_USER_LOGIN_SELECT", new Dictionary<string, object>() {
      { "USER_ID","test" },{ "PASSWORD","x" },{ "IS_HAN","H" }
    });

    string token = await PostMethodStr("api/Account/Login", req);


    if (!string.IsNullOrEmpty(token)) {
      _sess.SetItemAsync("ums_token", token);
    }

  }

  Req CreateReq(string qName, Dictionary<string, object> dic) {
    Req req = new Req() { QueryName = qName };
    if (dic != null && dic.Count > 0) {
      foreach (var d in dic) {
        req.QueryParameters.Add(new QueryParameter() { ParameterName = d.Key, ParameterValue = d.Value });
      }
    }
    return req;
  }


}



public interface IUMSService {
  Task<string> GetData(string path, Dictionary<string, object> dic);
  Task Login();
}