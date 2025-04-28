using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;
using System.Text;

namespace backend.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class JsonSchemaValidationAttribute : ActionFilterAttribute
    {
        private readonly string _schemaPath;

        public JsonSchemaValidationAttribute(string schemaPath)
        {
            _schemaPath = schemaPath;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            context.HttpContext.Request.EnableBuffering();

            string bodyString;
            var request = context.HttpContext.Request;

            // Enable buffering if not already done
            if (!request.Body.CanSeek)
            {
                request.EnableBuffering();
            }

            // Reset position to ensure we can read
            request.Body.Position = 0;

            // Read the body
            using (var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true))
            {
                bodyString = await reader.ReadToEndAsync();
            }

            // Reset again for MVC
            request.Body.Position = 0;

            Console.WriteLine($"Body: {bodyString}"); // Debug

            try
            {
                var rootPath = AppDomain.CurrentDomain.BaseDirectory;
                var fullPath = Path.Combine(rootPath, _schemaPath);

                if (!File.Exists(fullPath))
                {
                    context.Result = new BadRequestObjectResult(new { error = $"Schema file not found: {_schemaPath}" });
                    return;
                }

                var schemaJson = await File.ReadAllTextAsync(fullPath);
                var schema = JSchema.Parse(schemaJson);

                if (string.IsNullOrWhiteSpace(bodyString))
                {
                    context.Result = new BadRequestObjectResult(new { error = "Request body is empty" });
                    return;
                }

                var jsonObject = JObject.Parse(bodyString);

                if (!jsonObject.IsValid(schema, out IList<string> errorMessages))
                {
                    Console.WriteLine($"Schema Errors: {string.Join(", ", errorMessages)}"); // Debug log
                    context.Result = new BadRequestObjectResult(new { errors = errorMessages });
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Validation Error: {ex}"); // Debug log
                context.Result = new BadRequestObjectResult(new { error = $"Schema validation error: {ex.Message}" });
                return;
            }

            await next();
        }
    }
}
