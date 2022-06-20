package com.kino24.kino24newsread.spring.config;

import graphql.scalars.ExtendedScalars;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.graphql.execution.RuntimeWiringConfigurer;

@Configuration
public class GraphQLConfig {
    @Bean
    public RuntimeWiringConfigurer timeStampScalarType() {
        return builder -> builder.scalar(
                ExtendedScalars.newAliasedScalar("Timestamp")
                        .aliasedScalar(ExtendedScalars.DateTime)
                        .build()
        );
    }
}
