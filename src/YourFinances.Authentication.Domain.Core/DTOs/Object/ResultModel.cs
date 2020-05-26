using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace YourFinances.Authentication.Domain.Core.DTOs.Object
{
    public class ResultModel<T> : ValidateModel
    {
        public ResultModel(bool sucess = true) : base(sucess)
        {

        }

        [JsonProperty]
        public T Data { get; private set; }

        public void SetData(T data) => Data = data;
    }
}
