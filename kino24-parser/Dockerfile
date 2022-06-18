FROM openjdk:17.0.2-jdk as build
RUN mkdir -p /usr/app
WORKDIR /usr/app
COPY ./ /usr/app
RUN ./gradlew build -x test

FROM selenium/standalone-chrome:98.0
WORKDIR /usr/app
COPY --from=build /usr/app/build/libs/kino24-parser-0.0.1-SNAPSHOT.jar /usr/app/kino24-parser.jar
RUN sudo apt-get update && \
    sudo apt-get install -y openjdk-17-jre
EXPOSE 8080
ENTRYPOINT ["java","-jar","/usr/app/kino24-parser.jar"]