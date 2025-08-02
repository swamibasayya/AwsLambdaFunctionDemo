using Amazon.Lambda.Core;
using Amazon.S3;
using Amazon.S3.Model;
using System.Net.NetworkInformation;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace MyLambdaFunctionDemo;

public class Function
{

    private readonly IAmazonS3 _s3Client;
    public Function()
    {
        _s3Client=new AmazonS3Client();

    }


    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input">The event for the Lambda function handler to process.</param>
    /// <param name="context">The ILambdaContext that provides methods for logging and describing the Lambda environment.</param>
    /// <returns></returns>
    public async  Task<string> FunctionHandler(object input,  ILambdaContext context)
    {
        context.Logger.LogLine("FunctionHandler invoked");


        var bucketName = "rosterdata2025";
        var key = $"Crew-Data-{DateTime.UtcNow:yyyy-MM-dd-HH-mm-ss}.txt";
        var content = "Sample content from .NET Lambda";


        for (int i = 0; i < 5; i++)
        {


            var putRequest = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = key,
                ContentBody = content,
                ContentType = "text/plain"
            };
            context.Logger.LogLine($"File name{key}");

            try
            {
                var response = await _s3Client.PutObjectAsync(putRequest);
                return $"Uploaded to s3://{bucketName}/{key}";
            }

            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
           
        }
        return $"Uploaded to s3://{bucketName}/{key}";





    }
}
