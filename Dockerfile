FROM microsoft/dotnet:2.1-sdk-bionic

LABEL maintainer="Acid Chicken (硫酸鶏) <root@acid-chicken.com>"

RUN apt-get update \
 && apt-get upgrade -y \
 && apt-get install -y software-properties-common \
 && add-apt-repository universe \
 && apt-get update \
 && apt-get install -y zsh clang libcurl4-openssl-dev zlib1g-dev libkrb5-dev \
 && apt-get autoremove -y
