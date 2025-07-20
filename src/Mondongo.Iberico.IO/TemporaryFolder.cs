// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="TemporaryFolder.cs" company="OscarNET-SOFTware">
// ···
//      Mondongo.Dehesa.Framework - Just a set of essential libraries for DotNET: clean, simple and ready to use.
// ···
//      Copyright (c) 2025 Oscar Fernandez Gonzalez a.k.a. Osc@rNET
//      Licensed under the MIT License. See the 'LICENSE.md' file for details.
// ···
//      Third-party components are used in this project. For full license texts,
//      see the 'licenses' folder and the 'THIRD-PARTY-NOTICES.md' file.
// ···
// </copyright>
// ---------------------------------------------------------------------------------------------------------------------

using System.Diagnostics;
using System.IO;
using System.Text;

using Mondongo.Iberico.Extensions;
using Mondongo.Iberico.IO.Resources;

namespace Mondongo.Iberico.IO;

/// <summary>
/// Represents a temporary folder.
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public class TemporaryFolder : IDisposable
{
    /// <summary>
    /// Stores the value indicating whether this instante is disposed.
    /// </summary>
    private volatile bool _isDisposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="TemporaryFolder" /> class.
    /// </summary>
    public TemporaryFolder()
    {
        FolderInfo = new TemporaryFolderInfo();
    }

    private string DebuggerDisplay => $"({nameof(TemporaryFolder)}) => FullName : \"{FolderInfo.FullName}\"";

    /// <summary>
    /// Get the folder information.
    /// </summary>
    /// <value>
    /// A <see cref="TemporaryFolderInfo" /> object that represents the folder information.
    /// </value>
    public TemporaryFolderInfo FolderInfo { get; }

    /// <summary>
    /// Removes all subfolders and files that are contained in this <see cref="TemporaryFolder" /> instance.
    /// </summary>
    public virtual void Clear()
    {
        EnsureHasNotBeenDisposedAndStillExists(IOResources.TemporaryFolderCouldNotBeClear);

        foreach (FileInfo fileInfo in FolderInfo.EnumerateFiles())
        {
            fileInfo.Delete();
        }

        foreach (DirectoryInfo subfolderInfo in FolderInfo.EnumerateFolders())
        {
            subfolderInfo.Delete(true);
        }
    }

    /// <summary>
    /// Combines the temporary folder full path with a given path.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns>
    /// The combined full path.
    /// </returns>
    public virtual string Combine(string path)
    {
        if (!path.IsValidFullPath())
        {
            throw new ArgumentException(
                message: IOResources.PathIsNotValid.FormatMessage(path),
                paramName: nameof(path));
        }

        return Path.Combine(FolderInfo.FullName, path.Trim());
    }

    /// <summary>
    /// Creates a file in this temporary folder. If the file already exists, it will be overwritten.
    /// </summary>
    /// <param name="fileName">The optional file name. Default it's a random file name.</param>
    /// <returns>
    /// A <see cref="FileStream" /> instance pointing to the created file.
    /// </returns>
    public virtual FileStream CreateFile(string? fileName = null)
    {
        EnsureHasNotBeenDisposedAndStillExists(IOResources.TemporaryFolderFileCouldNotBeCreate);

        string filePath = string.IsNullOrWhiteSpace(fileName)
            ? Combine(Path.GetRandomFileName())
            : Combine(fileName);

        return new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Read);
    }

    /// <summary>
    /// Creates a text file in this temporary folder. If the file already exists, it will be overwritten.
    /// </summary>
    /// <param name="encoding">The optional encoding of the file. Default it's UTF-8.</param>
    /// <param name="fileName">The optional file name. Default it's a random file name.</param>
    /// <returns>
    /// A <see cref="StreamWriter" /> instance pointing to the created file.
    /// </returns>
    public virtual StreamWriter CreateTextFile(Encoding? encoding = null, string? fileName = null)
    {
        EnsureHasNotBeenDisposedAndStillExists(IOResources.TemporaryFolderFileCouldNotBeCreate);

        Encoding fileEncoding = encoding ?? Encoding.UTF8;

        string filePath = string.IsNullOrWhiteSpace(fileName)
            ? Combine(Path.GetRandomFileName())
            : Combine(fileName);

        return fileEncoding != Encoding.UTF8
            ? new StreamWriter(filePath, false, fileEncoding)
            : File.CreateText(filePath);
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources.
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources;
    /// <c>false</c> to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (_isDisposed)
        {
            return;
        }

        if (disposing && FolderInfo.Exists)
        {
            FolderInfo.Delete(true);
        }

        _isDisposed = true;
    }

    /// <summary>
    /// Ensures that this instance has not been disposed and that it still exists.
    /// </summary>
    /// <param name="errorMessage">The error message.</param>
    /// <exception cref="InvalidOperationException">
    /// This instance has been disposed or the temporary folder does not exist.
    /// </exception>
    protected void EnsureHasNotBeenDisposedAndStillExists(string errorMessage)
    {
        if (_isDisposed)
        {
            throw new InvalidOperationException(
                errorMessage.FormatMessage(
                    FolderInfo.FullName, IOResources.TemporaryFolderHasBeenDisposed));
        }

        if (!FolderInfo.Exists)
        {
            throw new InvalidOperationException(
                errorMessage.FormatMessage(
                    FolderInfo.FullName, IOResources.TemporaryDirectoryHasAlreadyBeenDeleted));
        }
    }
}
