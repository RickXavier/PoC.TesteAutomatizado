SonarQube.Scanner.MSBuild.exe begin /k:"poc.testeautomatizado" /n:"PoC.TesteAutomatizado" /v:"master"
MSBuild.exe /t:Rebuild
SonarQube.Scanner.MSBuild.exe end