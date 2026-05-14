using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFix.Application.Common.Errors;
using AutoFix.Application.Common.Interfaces;
using AutoFix.Domain.Common.Results;
using AutoFix.Domain.Customers;
using AutoFix.Domain.Customers.Vehicles;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;

namespace AutoFix.Application.Features.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler(ILogger<UpdateCustomerCommandHandler> logger,IAppDbContext context,HybridCache cache) : IRequestHandler<UpdateCustomerCommand, Result<Updated>>
    {
        private readonly ILogger<UpdateCustomerCommandHandler> _logger = logger;
        private readonly IAppDbContext _context = context;
        private readonly HybridCache _cache = cache;

        public async Task<Result<Updated>> Handle(UpdateCustomerCommand command, CancellationToken ct)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == command.CustomerId,ct);

            if (customer is null)
            {
                _logger.LogWarning("Customer {CustomerId} not found for update.", command.CustomerId);

                return ApplicationErrors.CustomerNotFound;
            }
            var validatedVehicles = new List<Vehicle>();

            foreach (var vehicle in command.Vehicles)
            {
                var vehicleId=vehicle.VehicleId?? Guid.NewGuid();

                var vehicleResult = Vehicle.Create(vehicleId, vehicle.Make, vehicle.Model, vehicle.Year, vehicle.LicensePlate);

                if (vehicleResult.IsError)
                {
                    return vehicleResult.Errors;
                }

                validatedVehicles.Add(vehicleResult.Value);
            }
            var updateCustomerResult = customer.Update(command.Name, command.Email, command.PhoneNumber);
            if (updateCustomerResult.IsError)
            {
                return updateCustomerResult.Errors;
            }

            var upsertPartResult = customer.UpserParts(validatedVehicles);
            if (upsertPartResult.IsError)
            {
                return upsertPartResult.Errors;
            }
            await _context.SaveChangesAsync(ct);
            await _cache.RemoveByTagAsync("customer", ct);

            return Result.Updated;
        }
    }
}
