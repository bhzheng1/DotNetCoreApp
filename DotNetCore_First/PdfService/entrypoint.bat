set ASPNETCORE_URLS=http://+:#{port}#
set ASPNETCORE_ENVIRONMENT=#{Release.EnvironmentName}#
set VaultService__SecretId=#{VaultSecretId}#
IDB.WatermarksService.exe