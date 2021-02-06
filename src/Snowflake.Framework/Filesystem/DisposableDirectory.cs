﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snowflake.Filesystem
{
    internal class DisposableDirectory
        : IDisposableDirectory
    {
        private bool disposedValue;

        public DisposableDirectory(Directory baseDirectory)
        {
            this.Base = baseDirectory;
        }

        public Directory Base { get; }

        public string Name => this.Base.Name;

        public bool ContainsDirectory(string directory) => this.Base.ContainsDirectory(directory);

        public bool ContainsFile(string file) => this.Base.ContainsDirectory(file);

        public IFile CopyFrom(System.IO.FileInfo source) => this.Base.CopyFrom(source);

        public IFile CopyFrom(System.IO.FileInfo source, bool overwrite) => this.Base.CopyFrom(source, overwrite);

        public IFile CopyFrom(IReadOnlyFile source) => this.Base.CopyFrom(source);

        public IFile CopyFrom(IReadOnlyFile source, bool overwrite) => this.Base.CopyFrom(source);

        public Task<IFile> CopyFromAsync(System.IO.FileInfo source, CancellationToken cancellation = default)
                => this.Base.CopyFromAsync(source, cancellation);

        public Task<IFile> CopyFromAsync(System.IO.FileInfo source, bool overwrite, CancellationToken cancellation = default)
            => this.Base.CopyFromAsync(source, overwrite, cancellation);

        public Task<IFile> CopyFromAsync(IReadOnlyFile source, CancellationToken cancellation = default)
            => this.Base.CopyFromAsync(source, cancellation);

        public Task<IFile> CopyFromAsync(IReadOnlyFile source, bool overwrite, CancellationToken cancellation = default)
            => this.Base.CopyFromAsync(source, overwrite, cancellation);

        public IEnumerable<IFile> EnumerateFiles() => this.Base.EnumerateFiles();

        public IEnumerable<IFile> EnumerateFilesRecursive() => this.Base.EnumerateFilesRecursive();

        public IEnumerable<IProjectingDirectory> EnumerateDirectories()
        {
            throw new NotImplementedException();
        }

        public IProjectingDirectory OpenDirectory(string name)
        {
            throw new NotImplementedException();
        }

        public IFile OpenFile(string file) => this.Base.OpenFile(file);

        public System.IO.DirectoryInfo UnsafeGetPath() => this.Base.UnsafeGetPath();

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.Base.Delete();
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
