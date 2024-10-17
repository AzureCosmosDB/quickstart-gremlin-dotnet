    TableServiceClient serviceClient = new(
        connectionString: "<azure-cosmos-db-table-connection-string>"
    );

    TableClient client = serviceClient.GetTableClient(
        tableName: "<table-name>"
    );
