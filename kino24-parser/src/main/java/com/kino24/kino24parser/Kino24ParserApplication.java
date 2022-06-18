package com.kino24.kino24parser;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.context.properties.ConfigurationPropertiesScan;
import org.springframework.scheduling.annotation.EnableScheduling;

@EnableScheduling
@ConfigurationPropertiesScan
@SpringBootApplication
public class Kino24ParserApplication {

	public static void main(String[] args) {
		SpringApplication.run(Kino24ParserApplication.class, args);
	}

}
