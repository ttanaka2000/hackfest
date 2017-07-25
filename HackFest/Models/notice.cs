using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Table; // Namespace for Table storage types

namespace HackFest.Models
{
    public class notice
    {
        const string TABLE_NAME = "msgTable";

        static public List<iMsgData> enumMsgData(string userId) {
            CloudTable table = getTableInstanse();
            TableQuery<msgData> query = new TableQuery<msgData>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, userId));
            List<iMsgData> msgList = new List<iMsgData>();
            // Print the fields for each customer.
            foreach (msgData entity in table.ExecuteQuery(query))
            {
                iMsgData ientity = new iMsgData();
                ientity.userId = entity.PartitionKey;
                ientity.text = entity.text;
                ientity.receiveTime = entity.receiveTime;
                msgList.Add(ientity);

                TableOperation deleteOperation = TableOperation.Delete(entity);
                // Execute the operation.
                table.Execute(deleteOperation);
            }
            return msgList;
        }

        //通知データは保持する
        static public void addMsgData(string userName, string msgText) {
            Guid guidValue = Guid.NewGuid();
            CloudTable table = getTableInstanse();
            msgData msgDataEntity = new msgData(userName, guidValue.ToString());
            msgDataEntity.text = msgText;
            msgDataEntity.receiveTime = DateTime.Now;
            TableOperation insertOperation = TableOperation.Insert(msgDataEntity);
            // Execute the insert operation.
            table.Execute(insertOperation);
        }

        static CloudTable getTableInstanse() {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference(TABLE_NAME);
            return table;
        }
    }


    public class iMsgData {
        public string userId;
        public string text;
        public DateTime receiveTime;
    }


    public class msgData : TableEntity
    {
        public msgData(string PartitionKey, string RowKey)
        {
            this.PartitionKey = PartitionKey;
            this.RowKey = RowKey;
        }

        public msgData() { }

        //public string userId { get; set; }
        public string text { get; set; }

        public DateTime receiveTime { get; set; }

    }
}