﻿using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace times.Tests.Helpers
{
    public class MockCloudTableConsolidated : CloudTable
    {
        public MockCloudTableConsolidated(Uri tableAddress) : base(tableAddress)
        {
        }

        public MockCloudTableConsolidated(Uri tableAbsoluteUri, StorageCredentials credentials) : base(tableAbsoluteUri, credentials)
        {
        }

        public MockCloudTableConsolidated(StorageUri tableAddress, StorageCredentials credentials) : base(tableAddress, credentials)
        {
        }

        public override async Task<TableQuerySegment<TimeEntity>> ExecuteQuerySegmentedAsync<TimeEntity>(TableQuery<TimeEntity> query, TableContinuationToken token)
        {
            ConstructorInfo constructor = typeof(TableQuerySegment<TimeEntity>)
                   .GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic)
                   .FirstOrDefault(c => c.GetParameters().Count() == 1);

            return await Task.FromResult(constructor.Invoke(new object[] { TestFactory.GetConsolidatedList() }) as TableQuerySegment<TimeEntity>);
        }
    }
}
