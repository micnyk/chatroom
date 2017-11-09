FROM microsoft/aspnetcore:2.0


RUN apt-get update && apt-get install -y gnupg1 && curl -sL https://deb.nodesource.com/setup_7.x | bash -
RUN apt-get install -y build-essential nodejs

ARG source
ARG ASPNETCORE_ENVIRONMENT

EXPOSE 80

WORKDIR /app
COPY ${source:-bin/${ASPNETCORE_ENVIRONMENT:-Debug}/netcoreapp2.0/publish} .

ENTRYPOINT ["dotnet", "ChatRoom.Web.dll"]
