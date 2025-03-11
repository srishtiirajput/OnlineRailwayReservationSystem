using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.Results;
using RailwayReservation.Models;
using RailwayReservation.Interfaces;

public class TrainRepository : ITrain
{
    private readonly OnlineRailwayReservationSystemDbContext _context;

    private readonly TrainValidator _validator = new();

    public TrainRepository(OnlineRailwayReservationSystemDbContext context)

    {

        _context = context;

    }

    // 🟢 GET ALL TRAINS

    public async Task<IEnumerable<Train>> GetAllTrainsAsync()

    {

        return await _context.Trains.ToListAsync();

    }

    // 🟢 GET TRAIN BY ID

    public async Task<Train> GetTrainByIdAsync(string trainId)

    {

        ValidateTrainId(trainId);

        var train = await _context.Trains.FindAsync(trainId);

        if (train == null)

            throw new KeyNotFoundException($"Train with ID '{trainId}' not found.");

        return train;

    }

    // 🟢 ADD TRAIN (WITH VALIDATION)

    public async Task AddTrainAsync(Train train)

    {

        await ValidateTrainAsync(train);

        bool exists = await _context.Trains.AnyAsync(t => t.TrainId == train.TrainId);

        if (exists)

            throw new InvalidOperationException($"A train with ID '{train.TrainId}' already exists.");

        await _context.Trains.AddAsync(train);

        await _context.SaveChangesAsync();

    }

    // 🟢 UPDATE TRAIN (WITH VALIDATION)

    public async Task UpdateTrainAsync(string trainId, Train train)

    {

        ValidateTrainId(trainId);

        await ValidateTrainAsync(train);

        var existingTrain = await _context.Trains.FindAsync(trainId);

        if (existingTrain == null)

            throw new KeyNotFoundException($"Train with ID '{trainId}' not found.");

        existingTrain.TrainNumber = train.TrainNumber;

        existingTrain.TrainName = train.TrainName;

        existingTrain.RunningDay = train.RunningDay;

        existingTrain.TrainRoute = train.TrainRoute;

        await _context.SaveChangesAsync();

    }

    // 🟢 DELETE TRAIN

    public async Task DeleteTrainAsync(string trainId)

    {

        ValidateTrainId(trainId);

        var train = await _context.Trains.FindAsync(trainId);

        if (train == null)

            throw new KeyNotFoundException($"Train with ID '{trainId}' not found.");

        _context.Trains.Remove(train);

        await _context.SaveChangesAsync();

    }

    // 🟢 GET TRAINS BY ROUTE

    public async Task<IEnumerable<Train>> GetTrainsByRouteAsync(string trainRoute)

    {

        if (string.IsNullOrWhiteSpace(trainRoute))

            throw new ArgumentException("Route cannot be null or empty.");

        return await _context.Trains.Where(t => t.TrainRoute == trainRoute).ToListAsync();

    }

    // 🟢 GET TRAINS BY RUNNING DAY

    public async Task<IEnumerable<Train>> GetTrainsByRunningDayAsync(string runningDay)

    {

        if (string.IsNullOrWhiteSpace(runningDay))

            throw new ArgumentException("Running day cannot be null or empty.");

        return await _context.Trains

            .Where(t => t.RunningDay != null && t.RunningDay.Contains(runningDay))

            .ToListAsync();

    }

    // 🟢 PRIVATE: Validate Train ID Format

    private void ValidateTrainId(string trainId)

    {

        if (string.IsNullOrWhiteSpace(trainId))

            throw new ArgumentException("Train ID cannot be null or empty.");

    }

    // 🟢 PRIVATE: Validate Train Data (Using FluentValidation)

    private async Task ValidateTrainAsync(Train train)

    {

        ValidationResult validationResult = await _validator.ValidateAsync(train);

        if (!validationResult.IsValid)

        {

            throw new ValidationException(validationResult.Errors);

        }

    }

}

