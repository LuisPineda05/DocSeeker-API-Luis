using AutoMapper;
using DocSeeker.API.DocSeeker.Domain.Repositories;
using DocSeeker.API.Security.Authorization.Handlers.Interfaces;
using DocSeeker.API.Security.Domain.Models;
using DocSeeker.API.Security.Domain.Repositories;
using DocSeeker.API.Security.Domain.Services;
using DocSeeker.API.Security.Domain.Services.Communication;
using DocSeeker.API.Security.Exceptions;
using DocSeeker.API.Shared.Domain.Repositories;
using BCryptNet = BCrypt.Net.BCrypt;

namespace DocSeeker.API.Security.Services;

public class UserService : IUserService
{

    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    private readonly IJwtHandler _jwtHandler;
    private readonly IMapper _mapper;

    public UserService(IJwtHandler jwtHandler,
        IMapper mapper, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _jwtHandler = jwtHandler;
        _mapper = mapper;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
    {
        var user = await _userRepository.FindByDniAsync(request.Dni);
        Console.WriteLine($"Request: {request.Dni}, {request.Password}");
        Console.WriteLine($"User: {user.Id}, {user.FirstName}, {user.LastName}, {user.Dni}, {user.Birthday}, {user.cell1}, {user.Email}, {user.Password}");


        // validate
        if (user == null || !BCryptNet.Verify(request.Password, user.Password))
        {
            Console.WriteLine("Authentication Error");
            throw new AppException("Username or password is incorrect");
        }

        Console.WriteLine("Authentication successful. About to generate token");
        // authentication successful
        var response = _mapper.Map<AuthenticateResponse>(user);
        Console.WriteLine($"Response: {response.Id}, {response.FirstName}, {response.LastName}, {response.Dni}");
        response.Token = _jwtHandler.GenerateToken(user);
        Console.WriteLine($"Generated token is {response.Token}");
        return response;
    }

    public async Task<IEnumerable<User>> ListAsync()
    {
        return await _userRepository.ListAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        var user = await _userRepository.FindByIdAsync(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }

    public async Task RegisterAsync(RegisterRequest request)
    {
        // validate
        if (_userRepository.ExistsByDni(request.Dni))
            throw new AppException("DNI '" + request.Dni + "' is already taken");

        // map model to new user object
        var user = _mapper.Map<User>(request);

        // hash password
        user.Password = BCryptNet.HashPassword(request.Password);

        // save user
        try
        {
            await _userRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while saving the user: {e.Message}");
        }
    }

    public async Task UpdateAsync(int id, UpdateRequest request)
    {
        var user = GetById(id);

        // Validate
        if (_userRepository.ExistsByDni(request.Dni))
            throw new AppException("DNI '" + request.Dni + "' is already taken");

        // Hash password if it was entered
        if (!string.IsNullOrEmpty(request.Password))
            user.Password = BCryptNet.HashPassword(request.Password);

        // Copy model to user and save
        _mapper.Map(request, user);
        try
        {
            _userRepository.Update(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while updating the user: {e.Message}");
        }
    }

    public async Task DeleteAsync(int id)
    {
        var user = GetById(id);

        try
        {
            _userRepository.Remove(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while deleting the user: {e.Message}");
        }
    }

    // helper methods

    private User GetById(int id)
    {
        var user = _userRepository.FindById(id);
        if (user == null) throw new KeyNotFoundException("User not found");
        return user;
    }
}
