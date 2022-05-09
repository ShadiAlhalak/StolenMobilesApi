Techniques used: Asp core Api dotnet 5 with Entity Framwork 
steps to use this api:
1-Change sql connection in Models.StolenContext.OnConfiguring to your sql Connection 
2-in Tools-> Package manager console write : 
  1)Add-Migration initialDatabase 
  2)Update-database
3-Fuction name and parametars is clear to use and you can find comment in the code 
