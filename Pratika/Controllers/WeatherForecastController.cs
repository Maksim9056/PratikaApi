using Microsoft.AspNetCore.Mvc;
using Pratika.ClassForTest.Address;
using Pratika.ClassForTest.Order;
using Pratika.ClassForTest.Product;
using Pratika.ClassForTest.TaskItem;
using Pratika.ClassForTest.User;
using System.Diagnostics;
using System.Text.Json;

namespace Pratika.Controllers
{
    [ApiController]
    [Route("praticka")]
    
    public class Products : ControllerBase
    {
        private readonly string productsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "products.json");
        private List<Product> _products;

        public Products()
        {
            try
            {
                string productsData = System.IO.File.ReadAllText(productsFilePath);
                _products = JsonSerializer.Deserialize<List<Product>>(productsData) ?? new List<Product>();
            }
            catch (FileNotFoundException)
            {
                _products = new List<Product>();
                SaveChanges(); // Создаем пустой файл, если файл не найден
            }
        }

        private void SaveChanges()
        {
            var productsData = JsonSerializer.Serialize(_products);
            System.IO.File.WriteAllText(productsFilePath, productsData);
        }

        private void LogMethodExecutionTime(string methodName, Stopwatch stopwatch)
        {
            TimeSpan ts = stopwatch.Elapsed;
            string elapsedTime = $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}";
            Console.WriteLine($"Method: {methodName}, Execution Time: {elapsedTime}");
        }

        private IActionResult LogAndReturnResult<T>(string methodName, Stopwatch stopwatch, T result)
        {
            LogMethodExecutionTime(methodName, stopwatch);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            IEnumerable<Product> result = _products;
            return LogAndReturnResult("GetAllProducts", stopwatch, result);
        }

        [HttpPost("CreateProduct")]
        public IActionResult CreateProduct(Product product)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            product.Id = _products.Count + 1;
            _products.Add(product);
            SaveChanges();
            return LogAndReturnResult("CreateProduct", stopwatch, product);
        }

        [HttpGet("GetProductById/{id}")]
        public IActionResult GetProductById(int id)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var product = _products.FirstOrDefault(p => p.Id == id);
            return LogAndReturnResult("GetProductById", stopwatch, product);
        }

        [HttpPut("UpdateProduct/{id}")]
        public IActionResult UpdateProduct(int id, Product product)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var existingProduct = _products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Category = product.Category;

            SaveChanges();
            return LogAndReturnResult("UpdateProduct", stopwatch, existingProduct);
        }

        [HttpDelete("DeleteProduct/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            _products.Remove(product);
            SaveChanges();
            return LogAndReturnResult<IEnumerable<Product>>("DeleteProduct", stopwatch, _products);
        }
    }



    /// <summary>
    /// 
    /// </summary>
    public class Users : ControllerBase
    {
        private readonly string usersFilePath = Path.Combine(Directory.GetCurrentDirectory(), "users.json");
        private List<User> _users;

        public Users()
        {
            try
            {
                string usersData = System.IO.File.ReadAllText(usersFilePath);
                _users = JsonSerializer.Deserialize<List<User>>(usersData) ?? new List<User>();
            }
            catch (FileNotFoundException)
            {
                _users = new List<User>();
                SaveChanges(); // Создаем пустой файл, если файл не найден
            }
        }

        private void SaveChanges()
        {
            var usersData = JsonSerializer.Serialize(_users);
            System.IO.File.WriteAllText(usersFilePath, usersData);
        }

        private void LogMethodExecutionTime(string methodName, Stopwatch stopwatch)
        {
            TimeSpan ts = stopwatch.Elapsed;
            string elapsedTime = $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}";
            Console.WriteLine($"Method: {methodName}, Execution Time: {elapsedTime}");
        }

        private IActionResult LogAndReturnResult<T>(string methodName, Stopwatch stopwatch, T result)
        {
            LogMethodExecutionTime(methodName, stopwatch);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            IEnumerable<User> result = _users;
            return LogAndReturnResult("GetAllUsers", stopwatch, result);
        }

        [HttpPost("CreateUser")]
        public IActionResult CreateUser(User user)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            user.Id = _users.Count + 1;
            _users.Add(user);
            SaveChanges();
            return LogAndReturnResult("CreateUser", stopwatch, user);
        }

        [HttpGet("GetUserById/{id}")]
        public IActionResult GetUserById(int id)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var user = _users.FirstOrDefault(u => u.Id == id);
            return LogAndReturnResult("GetUserById", stopwatch, user);
        }

        [HttpPut("UpdateUser/{id}")]
        public IActionResult UpdateUser(int id, User user)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var existingUser = _users.FirstOrDefault(u => u.Id == id);
            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.Username = user.Username;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;

            SaveChanges();
            return LogAndReturnResult("UpdateUser", stopwatch, existingUser);
        }

        [HttpDelete("DeleteUser/{id}")]
        public IActionResult DeleteUser(int id)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            _users.Remove(user);
            SaveChanges();
            return LogAndReturnResult<IEnumerable<User>>("DeleteUser", stopwatch, _users);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Orders : ControllerBase
    {
        private readonly string ordersFilePath = Path.Combine(Directory.GetCurrentDirectory(), "orders.json");
        private List<Order> _orders;

        public Orders()
        {
            try
            {
                string ordersData = System.IO.File.ReadAllText(ordersFilePath);
                _orders = JsonSerializer.Deserialize<List<Order>>(ordersData) ?? new List<Order>();
            }
            catch (FileNotFoundException)
            {
                _orders = new List<Order>();
                SaveChanges(); // Создаем пустой файл, если файл не найден
            }
        }

        private void SaveChanges()
        {
            var ordersData = JsonSerializer.Serialize(_orders);
            System.IO.File.WriteAllText(ordersFilePath, ordersData);
        }

        private void LogMethodExecutionTime(string methodName, Stopwatch stopwatch)
        {
            TimeSpan ts = stopwatch.Elapsed;
            string elapsedTime = $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}";
            Console.WriteLine($"Method: {methodName}, Execution Time: {elapsedTime}");
        }

        private IActionResult LogAndReturnResult<T>(string methodName, Stopwatch stopwatch, T result)
        {
            LogMethodExecutionTime(methodName, stopwatch);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("GetAllOrders")]
        public IActionResult GetAllOrders()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            IEnumerable<Order> result = _orders;
            return LogAndReturnResult("GetAllOrders", stopwatch, result);
        }

        [HttpPost("CreateOrder")]
        public IActionResult CreateOrder(Order order)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            order.Id = _orders.Count + 1;
            _orders.Add(order);
            SaveChanges();
            return LogAndReturnResult("CreateOrder", stopwatch, order);
        }

        [HttpGet("GetOrderById/{id}")]
        public IActionResult GetOrderById(int id)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var order = _orders.FirstOrDefault(o => o.Id == id);
            return LogAndReturnResult("GetOrderById", stopwatch, order);
        }

        [HttpPut("UpdateOrder/{id}")]
        public IActionResult UpdateOrder(int id, Order order)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var existingOrder = _orders.FirstOrDefault(o => o.Id == id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            existingOrder.UserId = order.UserId;
            existingOrder.OrderDate = order.OrderDate;
            existingOrder.Products = order.Products;

            SaveChanges();
            return LogAndReturnResult("UpdateOrder", stopwatch, existingOrder);
        }

        [HttpDelete("DeleteOrder/{id}")]
        public IActionResult DeleteOrder(int id)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            _orders.Remove(order);
            SaveChanges();
            return LogAndReturnResult<IEnumerable<Order>>("DeleteOrder", stopwatch, _orders);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Adres : ControllerBase
    {
        private readonly string addressesFilePath = Path.Combine(Directory.GetCurrentDirectory(), "addresses.json");
        private List<Address> _addresses;

        public Adres()
        {
            try
            {
                string addressesData = System.IO.File.ReadAllText(addressesFilePath);
                _addresses = JsonSerializer.Deserialize<List<Address>>(addressesData) ?? new List<Address>();
            }
            catch (FileNotFoundException)
            {
                _addresses = new List<Address>();
                SaveChanges(); // Создаем пустой файл, если файл не найден
            }
        }

        private void SaveChanges()
        {
            var addressesData = JsonSerializer.Serialize(_addresses);
            System.IO.File.WriteAllText(addressesFilePath, addressesData);
        }

        private void LogMethodExecutionTime(string methodName, Stopwatch stopwatch)
        {
            TimeSpan ts = stopwatch.Elapsed;
            string elapsedTime = $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}";
            Console.WriteLine($"Method: {methodName}, Execution Time: {elapsedTime}");
        }

        private IActionResult LogAndReturnResult<T>(string methodName, Stopwatch stopwatch, T result)
        {
            LogMethodExecutionTime(methodName, stopwatch);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("GetAllAddresses")]
        public IActionResult GetAllAddresses()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            IEnumerable<Address> result = _addresses;
            return LogAndReturnResult("GetAllAddresses", stopwatch, result);
        }

        [HttpPost("CreateAddress")]
        public IActionResult CreateAddress(Address address)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            address.Id = _addresses.Count + 1;
            _addresses.Add(address);
            SaveChanges();
            return LogAndReturnResult("CreateAddress", stopwatch, address);
        }

        [HttpGet("GetAddressById/{id}")]
        public IActionResult GetAddressById(int id)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var address = _addresses.FirstOrDefault(a => a.Id == id);
            return LogAndReturnResult("GetAddressById", stopwatch, address);
        }

        [HttpPut("UpdateAddress/{id}")]
        public IActionResult UpdateAddress(int id, Address address)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var existingAddress = _addresses.FirstOrDefault(a => a.Id == id);
            if (existingAddress == null)
            {
                return NotFound();
            }

            existingAddress.Street = address.Street;
            existingAddress.City = address.City;
            existingAddress.ZipCode = address.ZipCode;

            SaveChanges();
            return LogAndReturnResult("UpdateAddress", stopwatch, existingAddress);
        }

        [HttpDelete("DeleteAddress/{id}")]
        public IActionResult DeleteAddress(int id)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var address = _addresses.FirstOrDefault(a => a.Id == id);
            if (address == null)
            {
                return NotFound();
            }

            _addresses.Remove(address);
            SaveChanges();
            return LogAndReturnResult<IEnumerable<Address>>("DeleteAddress", stopwatch, _addresses);
        }
    }


   /// <summary>
   /// 
   /// </summary>
    public class TaskItems : ControllerBase
    {
        private readonly string taskItemsFilePath;
        private List<TaskItem> _taskItems;
        public TaskItems()
        {
            taskItemsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "taskItems.json");

            try
            {
                string taskItemsData = System.IO.File.ReadAllText(taskItemsFilePath);
                _taskItems = JsonSerializer.Deserialize<List<TaskItem>>(taskItemsData) ?? new List<TaskItem>();
            }
            catch (FileNotFoundException)
            {
                _taskItems = new List<TaskItem>();
                SaveChanges(); // Создаем пустой файл, если файл не найден
            }
        }
        private void SaveChanges()
        {
            var taskItemsData = JsonSerializer.Serialize(_taskItems);
            System.IO.File.WriteAllText(taskItemsFilePath, taskItemsData);
        }
        private IActionResult LogAndReturnResult<T>(string methodName, Stopwatch stopwatch, T result)
        {
            TimeSpan ts = stopwatch.Elapsed;
            string elapsedTime = $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}";
            Console.WriteLine($"Method: {methodName}, Execution Time: {elapsedTime}");

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("GetAllTaskItems")]
        public IActionResult GetAllTaskItems()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            IEnumerable<TaskItem> result = _taskItems;
            return LogAndReturnResult("GetAllTaskItems", stopwatch, result);
        }

        [HttpPost("CreateTaskItem")]
        public IActionResult CreateTaskItem(TaskItem taskItem)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            taskItem.Id = _taskItems.Count + 1;
            _taskItems.Add(taskItem);
            SaveChanges();
            return LogAndReturnResult("CreateTaskItem", stopwatch, taskItem);
        }

        [HttpGet("GetTaskItemById/{id}")]
        public IActionResult GetTaskItemById(int id)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var taskItem = _taskItems.FirstOrDefault(t => t.Id == id);
            return LogAndReturnResult("GetTaskItemById", stopwatch, taskItem);
        }

        [HttpPut("UpdateTaskItem/{id}")]
        public IActionResult UpdateTaskItem(int id, TaskItem taskItem)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var existingTaskItem = _taskItems.FirstOrDefault(t => t.Id == id);
            if (existingTaskItem == null)
            {
                return NotFound();
            }

            existingTaskItem.Title = taskItem.Title;
            existingTaskItem.IsComplete = taskItem.IsComplete;
            SaveChanges();
            return LogAndReturnResult("UpdateTaskItem", stopwatch, existingTaskItem);
        }

        [HttpDelete("DeleteTaskItem/{id}")]
        public IActionResult DeleteTaskItem(int id)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            var taskItem = _taskItems.FirstOrDefault(t => t.Id == id);
            if (taskItem == null)
            {
                return NotFound();
            }

            _taskItems.Remove(taskItem);
            SaveChanges();
            return NoContent();
        }
    }
}
