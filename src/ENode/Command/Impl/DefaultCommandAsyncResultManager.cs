﻿using System;
using System.Collections.Concurrent;

namespace ENode.Commanding
{
    public class DefaultCommandAsyncResultManager : ICommandAsyncResultManager
    {
        private ConcurrentDictionary<Guid, CommandAsyncResult> _commandAsyncResultDict = new ConcurrentDictionary<Guid, CommandAsyncResult>();

        public void Add(Guid commandId, CommandAsyncResult commandAsyncResult)
        {
            if (!_commandAsyncResultDict.TryAdd(commandId, commandAsyncResult))
            {
                throw new Exception(string.Format("Command with id '{0}' is already exist.", commandId));
            }
        }
        public void TryComplete(Guid commandId, Exception exception)
        {
            CommandAsyncResult commandAsyncResult;
            if (_commandAsyncResultDict.TryRemove(commandId, out commandAsyncResult))
            {
                commandAsyncResult.Complete(exception);
            }
        }
    }
}
