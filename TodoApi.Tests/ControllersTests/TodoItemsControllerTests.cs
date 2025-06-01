using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Controllers;
using TodoApi.Data;
using TodoApi.Models;
using Xunit;

namespace TodoApi.Tests.ControllersTests
{
    public class TodoItemsControllerTests
    {
        private async Task<TodoContext> GetInMemoryDbContextAsync(string dbName)
        {
            var options = new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            var context = new TodoContext(options);
            // Seed initial data
            context.TodoItems.AddRange(new List<TodoItem>
            {
                new TodoItem { Title = "Task1", Description = "Desc1", IsCompleted = false },
                new TodoItem { Title = "Task2", Description = "Desc2", IsCompleted = true }
            });
            await context.SaveChangesAsync();
            return context;
        }

        [Fact]
        public async Task GetTodoItems_ReturnsAllItems()
        {
            // Arrange
            var context = await GetInMemoryDbContextAsync("GetAllDb");
            var controller = new TodoItemsController(context);

            // Act
            var result = await controller.GetTodoItems();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<TodoItem>>>(result);
            var items = Assert.IsAssignableFrom<IEnumerable<TodoItem>>(actionResult.Value!);
            Assert.Equal(2, items.Count());
        }

        [Fact]
        public async Task GetTodoItem_ExistingId_ReturnsItem()
        {
            // Arrange
            var context = await GetInMemoryDbContextAsync("GetByIdDb");
            var existingId = context.TodoItems.First().Id;
            var controller = new TodoItemsController(context);

            // Act
            var actionResult = await controller.GetTodoItem(existingId);

            // Assert
            var resultValue = Assert.IsType<TodoItem>(actionResult.Value!);
            Assert.Equal(existingId, resultValue.Id);
        }

        [Fact]
        public async Task GetTodoItem_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            var context = await GetInMemoryDbContextAsync("GetByIdDb2");
            var controller = new TodoItemsController(context);

            // Act
            var actionResult = await controller.GetTodoItem(999);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }

        [Fact]
        public async Task CreateTodoItem_ValidItem_ReturnsCreatedItem()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase(databaseName: "CreateDb")
                .Options;
            await using var context = new TodoContext(options);
            var controller = new TodoItemsController(context);
            var newItem = new TodoItem
            {
                Title = "NewTask",
                Description = "NewDesc",
                IsCompleted = false
            };

            // Act
            var actionResult = await controller.CreateTodoItem(newItem);

            // Assert
            var createdAtAction = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var item = Assert.IsType<TodoItem>(createdAtAction.Value!);
            Assert.Equal("NewTask", item.Title);
            Assert.Equal(1, context.TodoItems.Count());
        }

        [Fact]
        public async Task UpdateTodoItem_ValidUpdate_NoContentReturned()
        {
            // Arrange
            var context = await GetInMemoryDbContextAsync("UpdateDb");
            var existing = context.TodoItems.First();
            var controller = new TodoItemsController(context);

            var updated = new TodoItem
            {
                Id = existing.Id,
                Title = "UpdatedTitle",
                Description = "UpdatedDesc",
                IsCompleted = true
            };

            // Act
            var result = await controller.UpdateTodoItem(existing.Id, updated);

            // Assert
            Assert.IsType<NoContentResult>(result);
            var fromDb = await context.TodoItems.FindAsync(existing.Id);
            Assert.Equal("UpdatedTitle", fromDb.Title);
            Assert.True(fromDb.IsCompleted);
        }

        [Fact]
        public async Task UpdateTodoItem_MismatchedId_BadRequest()
        {
            // Arrange
            var context = await GetInMemoryDbContextAsync("UpdateDb2");
            var controller = new TodoItemsController(context);
            var updated = new TodoItem { Id = 999, Title = "X", Description = "Y", IsCompleted = false };

            // Act
            var result = await controller.UpdateTodoItem(1, updated);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("ID in URL does not match ID in body.", badRequest.Value);
        }

        [Fact]
        public async Task DeleteTodoItem_ExistingId_NoContentReturned()
        {
            // Arrange
            var context = await GetInMemoryDbContextAsync("DeleteDb");
            var existing = context.TodoItems.First();
            var controller = new TodoItemsController(context);

            // Act
            var result = await controller.DeleteTodoItem(existing.Id);

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Empty(context.TodoItems);
        }

        [Fact]
        public async Task DeleteTodoItem_NonExistingId_NotFoundReturned()
        {
            // Arrange
            var context = await GetInMemoryDbContextAsync("DeleteDb2");
            var controller = new TodoItemsController(context);

            // Act
            var result = await controller.DeleteTodoItem(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
