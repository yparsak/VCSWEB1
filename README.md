# VCSWEB1

C# WEB project that uses MVC and API frameworks. The MVC only has a default page. The following describes the API:

```
/api/Response?sp=STORED_PROCEDURE&ParamA=value&ParamB=value

/api/Response?query=SELECT+*+from+TableName

/api/Response?command=DROP+TABLE+TableName

```

**Warning** : This project is only for demonstration purpose. Query and Command APIs contain security risks, and they should be disabled (in Response Controller).

The API calls stored procedure using parameters described above. The result is returned as an XML document.

The format is:

```
<Response>
<data>
  <data>
     <columns>
         <ColumnA />
         <ColumnB />
     </columns>
     <rows>
       <row rowid='1' column='ColumnA' value='X'>X</row>
       <row rowid='1' column='ColumnB' value='Y'>Y</row>
     </rows>
  </data>
</data>
<status />
<type>success</type>
</Response>
```

To configure: you must modify Web.config and set ConStr (Connection String) variable.
