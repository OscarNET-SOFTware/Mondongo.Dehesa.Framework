// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="TemporaryFolderInfo.cs" company="OscarNET-SOFTware">
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
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;

namespace Mondongo.Iberico.IO;

/// <summary>
/// Represents the information of a temporary folder.
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class TemporaryFolderInfo : IEquatable<TemporaryFolderInfo>
{
    /// <summary>
    /// Defines the prefix of the temporary folder name.
    /// </summary>
    internal const string TemporaryFolderNamePrefix = "tmp-mondongo-";

    /// <summary>
    /// Stores the underlying <see cref="DirectoryInfo" /> object.
    /// </summary>
    private readonly DirectoryInfo _directoryInfo;

    /// <summary>
    /// Initializes a new instance of the <see cref="TemporaryFolderInfo" /> class.
    /// </summary>
    internal TemporaryFolderInfo()
    {
        _directoryInfo = Directory.CreateDirectory(FindFreeTemporaryFolderName());
    }

    [ExcludeFromCodeCoverage]
    private string DebuggerDisplay => $"({nameof(TemporaryFolderInfo)}) => FullName : \"{FullName}\"";

    /// <summary>
    /// Gets a value indicating whether the folder exists.
    /// </summary>
    /// <value>
    ///   <c>true</c> if the folder exists; otherwise, <c>false</c>.
    /// </value>
    public bool Exists => _directoryInfo.Exists;

    /// <summary>
    /// Gets the full path of the folder.
    /// </summary>
    /// <value>
    /// A <see cref="string" /> object containing the full path.
    /// </value>
    public string FullName => _directoryInfo.FullName;

    /// <summary>
    /// Gets the name of the folder.
    /// </summary>
    /// <value>
    /// A <see cref="string" /> object containing the name.
    /// </value>
    public string Name => _directoryInfo.Name;

    /// <summary>
    /// Creates a subfolder or subfolders on the specified path.
    /// The specified path can be relative to this instance of the <see cref="TemporaryFolderInfo" /> class.
    /// </summary>
    /// <param name="path">The specified path.
    /// This cannot be a different disk volume or Universal Naming Convention (UNC) name.</param>
    /// <returns>
    /// The last directory specified in <paramref name="path" />.
    /// </returns>
    public DirectoryInfo CreateSubfolder(string path)
        => _directoryInfo.CreateSubdirectory(path);

    /// <summary>
    /// Lists the files that are inside this temporary folder.
    /// </summary>
    /// <returns>
    /// An enumerable collection of <see cref="FileInfo" /> that represents the list of files inside.
    /// </returns>
    public IEnumerable<FileInfo> EnumerateFiles()
        => _directoryInfo.EnumerateFiles();

    /// <summary>
    /// Lists the files that are inside this temporary folder and that matches with a given search pattern.
    /// </summary>
    /// <param name="searchPattern">The search string to match against the names of files.
    /// This parameter can contain a combination of valid literal path and wildcard (* and ?) characters,
    /// but it doesn't support regular expressions.</param>
    /// <returns>
    /// An enumerable collection of <see cref="FileInfo" /> that represents the list of files inside
    /// that matches the specified <paramref name="searchPattern" />.
    /// </returns>
    public IEnumerable<FileInfo> EnumerateFiles(string searchPattern)
        => _directoryInfo.EnumerateFiles(searchPattern);

    /// <summary>
    /// Lists the files that are inside this temporary folder and that matches with a given search pattern
    /// and enumeration options.
    /// </summary>
    /// <param name="searchPattern">The search string to match against the names of files.
    /// This parameter can contain a combination of valid literal path and wildcard (* and ?) characters,
    /// but it doesn't support regular expressions.</param>
    /// <param name="enumerationOptions">An object that describes the search and enumeration
    /// configuration to use.</param>
    /// <returns>
    /// An enumerable collection of <see cref="FileInfo" /> that represents the list of files inside
    /// that matches the specified <paramref name="searchPattern" /> and <paramref name="enumerationOptions" />.
    /// </returns>
    public IEnumerable<FileInfo> EnumerateFiles(string searchPattern, EnumerationOptions enumerationOptions)
        => _directoryInfo.EnumerateFiles(searchPattern, enumerationOptions);

    /// <summary>
    /// Lists the files that are inside this temporary folder and that matches with a given search pattern
    /// and search subdirectory option.
    /// </summary>
    /// <param name="searchPattern">The search string to match against the names of files.
    /// This parameter can contain a combination of valid literal path and wildcard (* and ?) characters,
    /// but it doesn't support regular expressions.</param>
    /// <param name="searchOption">One of the enumeration values that specifies whether the search operation
    /// should include only the current directory or all subdirectories.</param>
    /// <returns>
    /// An enumerable collection of <see cref="FileInfo" /> that represents the list of files inside
    /// that matches the specified <paramref name="searchPattern" /> and <paramref name="searchOption" />.
    /// </returns>
    public IEnumerable<FileInfo> EnumerateFiles(string searchPattern, SearchOption searchOption)
        => _directoryInfo.EnumerateFiles(searchPattern, searchOption);

    /// <summary>
    /// Lists the folders that are inside this temporary folder.
    /// </summary>
    /// <returns>
    /// An enumerable collection of <see cref="DirectoryInfo" /> that represents the list of folders inside.
    /// </returns>
    public IEnumerable<DirectoryInfo> EnumerateFolders()
        => _directoryInfo.EnumerateDirectories();

    /// <summary>
    /// Lists the folders that are inside this temporary folder and that matches with a given search pattern.
    /// </summary>
    /// <param name="searchPattern">The search string to match against the names of folders.
    /// This parameter can contain a combination of valid literal path and wildcard (* and ?) characters,
    /// but it doesn't support regular expressions.</param>
    /// <returns>
    /// An enumerable collection of <see cref="DirectoryInfo" /> that represents the list of folders inside
    /// that matches the specified <paramref name="searchPattern" />.
    /// </returns>
    public IEnumerable<DirectoryInfo> EnumerateFolders(string searchPattern)
        => _directoryInfo.EnumerateDirectories(searchPattern);

    /// <summary>
    /// Lists the folders that are inside this temporary folder and that matches with a given search pattern
    /// and enumeration options.
    /// </summary>
    /// <param name="searchPattern">The search string to match against the names of folders.
    /// This parameter can contain a combination of valid literal path and wildcard (* and ?) characters,
    /// but it doesn't support regular expressions.</param>
    /// <param name="enumerationOptions">An object that describes the search and enumeration
    /// configuration to use.</param>
    /// <returns>
    /// An enumerable collection of <see cref="DirectoryInfo" /> that represents the list of folders inside
    /// that matches the specified <paramref name="searchPattern" /> and <paramref name="enumerationOptions" />.
    /// </returns>
    public IEnumerable<DirectoryInfo> EnumerateFolders(string searchPattern, EnumerationOptions enumerationOptions)
        => _directoryInfo.EnumerateDirectories(searchPattern, enumerationOptions);

    /// <summary>
    /// Lists the folders that are inside this temporary folder and that matches with a given search pattern
    /// and search subdirectory option.
    /// </summary>
    /// <param name="searchPattern">The search string to match against the names of folders.
    /// This parameter can contain a combination of valid literal path and wildcard (* and ?) characters,
    /// but it doesn't support regular expressions.</param>
    /// <param name="searchOption">One of the enumeration values that specifies whether the search operation
    /// should include only the current directory or all subdirectories.</param>
    /// <returns>
    /// An enumerable collection of <see cref="DirectoryInfo" /> that represents the list of folders inside
    /// that matches the specified <paramref name="searchPattern" /> and <paramref name="searchOption" />.
    /// </returns>
    public IEnumerable<DirectoryInfo> EnumerateFolders(string searchPattern, SearchOption searchOption)
        => _directoryInfo.EnumerateDirectories(searchPattern, searchOption);

    /// <summary>
    /// Determines whether the specified <see cref="object" />, is equal to this entity.
    /// </summary>
    /// <param name="obj">The <see cref="object" /> to compare with this entity.</param>
    /// <returns>
    ///   <c>true</c> if the specified <see cref="object" /> is equal to this entity; otherwise, <c>false</c>.
    /// </returns>
    public override bool Equals(object? obj) => Equals(obj as TemporaryFolderInfo);

    /// <summary>
    /// Indicates whether this entity is equal to another entity of the same type.
    /// </summary>
    /// <param name="other">An entity to compare with this entity.</param>
    /// <returns>
    ///   <c>true</c> if this entity is equal to the <paramref name="other">other</paramref> entity; otherwise, <c>false</c>.
    /// </returns>
    public bool Equals(TemporaryFolderInfo? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        StringComparison stringComparison = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
            ? StringComparison.InvariantCultureIgnoreCase
            : StringComparison.InvariantCulture;

        return string.Equals(FullName, other.FullName, stringComparison);
    }

    /// <summary>
    /// Returns a hash code for this entity.
    /// </summary>
    /// <returns>
    /// A hash code for this entity, suitable for use in hashing algorithms and data structures like a hash table.
    /// </returns>
    public override int GetHashCode() => FullName.GetHashCode();

    /// <summary>
    /// Deletes this instance of a <see cref="DirectoryInfo" />, specifying whether to delete subdirectories
    /// and files.
    /// </summary>
    /// <param name="recursive"><c>true</c> to delete this directory, its subdirectories, and all files;
    /// otherwise, <c>false</c>.</param>
    internal void Delete(bool recursive = false)
        => _directoryInfo.Delete(recursive);

    /// <summary>
    /// Finds a free temporary folder name.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> object containing the full path of a free temporary folder name.
    /// </returns>
    private static string FindFreeTemporaryFolderName()
    {
        Guid temporaryFolderId;
        string folderBasePath = Path.GetTempPath();
        string temporaryFolderFullPath, temporaryFolderName, temporaryFolderShortId;

        do
        {
            temporaryFolderId = Guid.NewGuid();
            temporaryFolderShortId = temporaryFolderId.ToString("N").Substring(0, 8);
            temporaryFolderName = $"{TemporaryFolderNamePrefix}{temporaryFolderShortId}";
            temporaryFolderFullPath = Path.Combine(folderBasePath, temporaryFolderName);
        }
        while (File.Exists(temporaryFolderFullPath) || Directory.Exists(temporaryFolderFullPath));

        return temporaryFolderFullPath;
    }
}
