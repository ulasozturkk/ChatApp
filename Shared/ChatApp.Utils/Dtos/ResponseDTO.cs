using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChatApp.Utils.Dtos {

  public class ResponseDTO<T> {
    public T? Data { get; set; }

    [JsonIgnore]
    public int? StatusCode { get; set; }

    [JsonIgnore]
    public bool? IsSuccessful { get; set; }

    public List<string>? Errors { get; set; }

    public static ResponseDTO<T> Success(T data, int statusCode) {
      return new ResponseDTO<T> { Data = data, StatusCode = statusCode, IsSuccessful = true };
    }

    public static ResponseDTO<T> Success(int statusCode) {
      return new ResponseDTO<T> { Data = default(T), StatusCode = statusCode, IsSuccessful = true };
    }

    public static ResponseDTO<T> Fail(List<string> errors, int statuscode) {
      return new ResponseDTO<T> { Errors = errors, StatusCode = statuscode, IsSuccessful = false };
    }

    public static ResponseDTO<T> Fail(string error, int statuscode) {
      var errors = new List<string> { error };
      return new ResponseDTO<T> { Errors = errors, StatusCode = statuscode, IsSuccessful = false };
    }
  }

}
