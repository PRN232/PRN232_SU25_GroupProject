# syntax=docker/dockerfile:1

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build

COPY . /source
WORKDIR /source/PRN232_SU25_GroupProject.Presentation

ARG TARGETARCH

RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish -a ${TARGETARCH/amd64/x64} --use-current-runtime --self-contained false -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app
COPY --from=build /app .

# ⚠️ Nếu bạn không khai báo `USER` riêng trong image base, nên bỏ dòng này
# USER $APP_UID

ENTRYPOINT ["dotnet", "PRN232_SU25_GroupProject.Presentation.dll"]
