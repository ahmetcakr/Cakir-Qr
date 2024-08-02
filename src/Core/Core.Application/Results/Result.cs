﻿namespace Core.Application.Results;
public sealed class Result<T>
{
    public T? Data { get; set; }

    public List<string>? ErrorMessages { get; set; }

    public bool IsSuccessful { get; set; } = true;


    public int StatusCode { get; set; } = 200;


    public Result(T data)
    {
        Data = data;
    }

    public Result(int statusCode, List<string> errorMessages)
    {
        IsSuccessful = false;
        StatusCode = statusCode;
        ErrorMessages = errorMessages;
    }

    public Result(int statusCode, string errorMessage)
    {
        IsSuccessful = false;
        StatusCode = statusCode;
        ErrorMessages = new List<string> { errorMessage };
    }

    public static implicit operator Result<T>(T data)
    {
        return new Result<T>(data);
    }

    public static implicit operator Result<T>((int statusCode, List<string> errorMessages) parameters)
    {
        return new Result<T>(parameters.statusCode, parameters.errorMessages);
    }

    public static implicit operator Result<T>((int statusCode, string errorMessage) parameters)
    {
        return new Result<T>(parameters.statusCode, parameters.errorMessage);
    }

    // this is a factory method
    // how to use
    // Result<CreatedCompanyResponse>.Succeed(response);
    // but if you wanna use with status code
    // Result<CreatedCompanyResponse>.Succeed(response, 201);

    public static Result<T> Succeed(T data)
    {
        return new Result<T>(data);
    }

    public static Result<T> Succeed(T data, int statusCode)
    {
        return new Result<T>(data);
    }

    public static Result<T> Failure(int statusCode, List<string> errorMessages)
    {
        return new Result<T>(statusCode, errorMessages);
    }

    public static Result<T> Failure(int statusCode, string errorMessage)
    {
        return new Result<T>(statusCode, errorMessage);
    }

    public static Result<T> Failure(string errorMessage)
    {
        return new Result<T>(500, errorMessage);
    }

    public static Result<T> Failure(List<string> errorMessages)
    {
        return new Result<T>(500, errorMessages);
    }
}
