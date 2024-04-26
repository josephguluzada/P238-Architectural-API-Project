namespace BlankSolution.Business.DTOs.UserDtos;

public record UserRegisterDto(string fullName, string userName, string email, string password, string confirmPassword);

