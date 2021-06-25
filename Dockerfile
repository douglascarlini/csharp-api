FROM mono

WORKDIR /app

COPY app.cs ./

RUN mcs -out:app.exe app.cs

EXPOSE 80
EXPOSE 433

CMD ["mono", "app.exe"]
