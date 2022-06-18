package com.kino24.kino24parser.core.domain.service.extractor;

import com.kino24.kino24parser.spring.config.Kino24Pages;
import com.kino24.kino24parser.core.domain.model.Kino24Article;
import io.github.bonigarcia.wdm.WebDriverManager;
import lombok.RequiredArgsConstructor;
import lombok.SneakyThrows;
import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.chrome.ChromeOptions;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.scheduling.annotation.Scheduled;
import org.springframework.stereotype.Service;

import javax.annotation.PostConstruct;
import java.util.Collection;
import java.util.List;
import java.util.Map;
import java.util.Optional;
import java.util.Set;
import java.util.concurrent.TimeUnit;
import java.util.stream.Collectors;

@Service
@RequiredArgsConstructor
public class Kino24Extractor {
    @Value("${kino24.url}")
    private final String kino24Url;
    private static final Logger LOG = LoggerFactory.getLogger(Kino24Extractor.class);
    public static final String NEWS_ITEM_CSS_SELECTOR = "app-publication-tag > div.grid-container div.grid-x.grid-padding-x .news-item:not(#mobileBrandingPlace34):not(.big)";

    private final Kino24Pages kino24Pages;
    private final List<Kino24Subscriber> newsSubscribers;

    private Map<String, String> categoryToPageMapping;

    @PostConstruct
    private void setup() {
        WebDriverManager.chromedriver().setup();
        categoryToPageMapping = Map.of(
                "MOVIES", kino24Pages.movies(),
                "SERIES", kino24Pages.series()
        );
    }

    @Scheduled(fixedDelay = 30, timeUnit = TimeUnit.MINUTES)
    @SneakyThrows
    private void extractNews() {
        LOG.info("Started extracting kino24 news");

        WebDriver driver = new ChromeDriver(new ChromeOptions().addArguments("headless"));

        var articlesByTitle = categoryToPageMapping.keySet().stream()
                .map(category -> extractArticles(driver, category))
                .flatMap(Collection::stream)
                .collect(Collectors.groupingBy(Kino24Article::title));

        var articles = articlesByTitle.values()
                .stream().map(Kino24Article::mergeCategories)
                .toList();

        LOG.info("{}", articles);
        LOG.info("{}", articles.size());

        driver.quit();

        newsSubscribers.forEach(subscriber -> subscriber.consume(articles));

        LOG.info("{} articles were extracted", articles.size());
    }

    @SneakyThrows
    private List<Kino24Article> extractArticles(WebDriver driver, String category) {
        driver.get(categoryToPageMapping.get(category));

        Thread.sleep(2000);

        var articles = driver
                .findElements(By.cssSelector(NEWS_ITEM_CSS_SELECTOR))
                .stream()
                .map(articleElement -> mapToArticleOpt(articleElement, category));

        return articles
                .filter(Optional::isPresent)
                .map(Optional::get)
                .toList();

    }

    private Optional<Kino24Article> mapToArticleOpt(WebElement articleElement, String category) {
        try {
            return Optional.of(mapToArticle(articleElement, category));
        } catch (RuntimeException e) {
            return Optional.empty();
        }
    }

    private Kino24Article mapToArticle(WebElement articleElement, String category) {
        var titleElement = articleElement.findElement(By.className("news-title"));
        var title = titleElement
                .findElement(By.tagName("h3"))
                .findElement(By.tagName("a")).getText().trim();

        var linkToOriginal = titleElement
                .findElement(By.tagName("h3"))
                .findElement(By.tagName("a")).getAttribute("href");

        var publicationDate = titleElement
                .findElement(By.className("time-date")).getText().trim();

        String author = "N/A";

        String publisher = "N/A";

        var imageLink = articleElement
                .findElement(By.className("photo"))
                .findElement(By.cssSelector(".image-wrapper img")).getAttribute("src");

        String content = "N/A";

        return Kino24Article.of(
                title,
                publicationDate,
                author,
                publisher,
                imageLink,
                content,
                linkToOriginal,
                Set.of(category)
        );
    }
}
