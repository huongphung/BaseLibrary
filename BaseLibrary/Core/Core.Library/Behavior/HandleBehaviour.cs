using Core.Library.Enums;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Library.Behavior
{
    public class HandleBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public HandleBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(
                    _validators.Select(v =>
                        v.ValidateAsync(context, cancellationToken)));

                var failures = validationResults
                    .Where(r => r.Errors.Any())
                    .SelectMany(r => r.Errors)
                    .ToList();

                if (failures.Any())
                    throw new ValidationException(EExceptionType.ValidationException.GetCode(), failures);
            }

            if (request is ICommand<TRequest>)
            {
                //using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
                //try
                //{
                //    var response = await next();
                //    await _context.SaveChangesAsync(cancellationToken);
                //    await transaction.CommitAsync(cancellationToken);
                //    return response;
                //}
                //catch
                //{
                //    await transaction.RollbackAsync(cancellationToken);
                //    throw new ;
                //}
            }

            return await next();
        }
    }

    // Marker interface for commands
    public interface ICommand<T> : IRequest<T> { }
}
