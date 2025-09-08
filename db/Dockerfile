# Use official SQL Server 2022 image
FROM mcr.microsoft.com/mssql/server:2022-latest

# Switch to root for installation
USER root

# Install prerequisites and mssql-tools
RUN apt-get update && \
    apt-get install -y curl apt-transport-https gnupg2 && \
    curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - && \
    curl https://packages.microsoft.com/config/ubuntu/22.04/prod.list > /etc/apt/sources.list.d/mssql-release.list && \
    apt-get update && \
    ACCEPT_EULA=Y apt-get install -y msodbcsql18 mssql-tools unixodbc-dev && \
    rm -rf /var/lib/apt/lists/*

# Add sqlcmd and bcp to PATH
ENV PATH="$PATH:/opt/mssql-tools/bin"

# Switch back to mssql user (required for SQL Server container)
USER mssql

# Expose SQL Server port
EXPOSE 1433

# Base image already has entrypoint and CMD for SQL Server