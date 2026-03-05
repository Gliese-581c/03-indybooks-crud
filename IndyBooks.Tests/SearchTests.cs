using IndyBooks.Services;
using IndyBooks.Models;
using IndyBooks.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace IndyBooks.Tests;

public class SearchTests
{
    private DbContextOptions<IndyBooksDataContext> _dbContextOptions;
    private Repository repository;

    public SearchTests()
    {
         _dbContextOptions = new DbContextOptionsBuilder<IndyBooksDataContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
           .Options;

        using(var context = new IndyBooksDataContext(_dbContextOptions))
        {

        }
    }
    [Fact]
   
    public void SaleResultCorrectlyCalculatesPrice()
    {
        // Arrange
        using var context = new IndyBooksDataContext(_dbContextOptions);
        repository = new Repository(context);
        var price = 101m;
        var sale = 0.20m;
        repository.SaleLimit = 100;
        repository.sale = sale;

        var book = context.Books.Where(b => b.Id == 4).Single();
        book.Price = price;
        context.Books.Update(book);
        context.SaveChanges();

        // Act
        var results = repository.SaleResults.ToList();

        // Assert
        Assert.Equal(results[0].Price, price * sale);
    }
    [Fact]
    public void SearchContainsBooksWithTitleContainingSearchTerm()
    {
        // Arrange
        using var context = new IndyBooksDataContext(_dbContextOptions);
        repository = new Repository(context);
        var searchVM = new SearchVM { Title = "Great" };

        // Act
        var results = repository.searchResults(searchVM).ToList();

        // Assert
        Assert.Single(results);
        Assert.Equal("The Great Gatsby", results[0].Title);
    }
    
    [Fact]
    public void SearchContainsAllBooksLessThanMaxPrice()
    {
        // Arrange
        using var context = new IndyBooksDataContext(_dbContextOptions);
        repository = new Repository(context);

        decimal maxPrice = 60m;
        var searchVM = new SearchVM { MaxPrice = maxPrice, MinPrice = 0 };
        var books = context.Books.Where(b=> b.Price < maxPrice);

        // Act
        var results = repository.searchResults(searchVM).ToList();

        // Assert
        Assert.Equal(books.Count(), results.Count());
    }

    

    
}

