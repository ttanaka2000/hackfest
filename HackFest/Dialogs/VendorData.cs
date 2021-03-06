﻿using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using HackFest.Dialogs;

namespace HackFest.Dialogs
{
    public class VendorData
    {
        public string retailId { get; set; }
        public string vendorId { get; set; }
        public string responseJson { get; set; }

        //コンストラクタ
        public VendorData(string[] args)
        {
            this.retailId = args[0];
            this.vendorId = args[1];
        }

        public void AuthVendorId()
        {
            string[] inputarray = new string[3];
            inputarray[0] = "dbo.authVendorId";
            inputarray[1] = this.retailId;
            inputarray[2] = this.vendorId;

            string procedureResult = AccessEdiDb.ExecProcedures(inputarray);
            var editResult = procedureResult.Split(new Char[] { '[', ']' });
            ResultVendorAuth resultVendor = JsonConvert.DeserializeObject<ResultVendorAuth>(editResult[1]);
            this.responseJson = resultVendor.result;

        }

        class ResultVendorAuth
        {
            public string result { get; set; }
            public string retailId { get; set; }
            public string vendorId { get; set; }
        }
    }
}
