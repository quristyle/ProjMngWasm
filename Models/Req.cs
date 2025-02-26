namespace ProjMngWasm.Models;

  public class Req {
    public string QueryName { get; set; }
    public List<QueryParameter> QueryParameters { get; set; } = new List<QueryParameter>();
    public bool ReturnQueryParameter { get; set; }
  }

  public class QueryParameter {
    public string ParameterName { get; set; }
    public object ParameterValue { get; set; }
    public int ParameterDirection { get; set; } = 1;
    public string Prefix { get; set; } = "IN_";
  }


