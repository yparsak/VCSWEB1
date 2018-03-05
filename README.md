# VCSWEB1

C# WEB project that uses MVC and API frameworks. The MVC only has a default page. The following describes the API:

```
/api/Response?sp=STORED_PROCEDURE&ParamA=value&ParamB=value
```

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
