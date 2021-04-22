FROM mcr.microsoft.com/mssql/server:2019-CU9-ubuntu-18.04

WORKDIR /usr/src/app

COPY . /usr/src/app

ENV SA_PASSWORD Password123
ENV ACCEPT_EULA Y
ENV MSSQL_PID Express

CMD /bin/bash ./entrypoint.sh