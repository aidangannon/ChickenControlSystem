﻿using Crosscutting.Services.Contract.HAL.Dto;

namespace Crosscutting.Services.Contract.HAL.Interface
{
    /// <summary>
    ///     performs the actions to send data over the control line
    /// </summary>
    public interface IControlLine
    {
        /// <summary>
        ///     sends an operation dto over to the control
        /// </summary>
        OperationResponseDto SendOperation(OperationDto operationDto);
    }
}