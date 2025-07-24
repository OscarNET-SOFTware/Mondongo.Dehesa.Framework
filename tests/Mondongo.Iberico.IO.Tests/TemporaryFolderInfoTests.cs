// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="TemporaryFolderInfoTests.cs" company="OscarNET-SOFTware">
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

using System.IO;

namespace Mondongo.Iberico.IO;

public sealed class TemporaryFolderInfoTests
{
    [Fact]
    public void Ctor_should_create_expected_temporary_folder()
    {
        var sut = new TemporaryFolderInfo();

        Assert.True(Directory.Exists(sut.FullName));
        Assert.True(sut.Exists);

        sut.Delete();
    }

    [Fact]
    public void Name_returns_expected_temporary_folder_name()
    {
        var sut = new TemporaryFolderInfo();
        string expectedName = Path.GetFileName(sut.FullName);

        Assert.Equal(expectedName, sut.Name);

        sut.Delete();
    }

    [Fact]
    public void Two_temporary_folders_are_not_equal()
    {
        object sutA = new TemporaryFolderInfo();
        var sutB = new TemporaryFolderInfo();

        Assert.False(sutA.Equals(sutB));

        ((TemporaryFolderInfo)sutA).Delete();
        sutB.Delete();
    }

    [Fact]
    public void Non_nullable_temporary_folder_and_temporary_folder_are_not_equal()
    {
        var sutA = new TemporaryFolderInfo();
        TemporaryFolderInfo? sutB = null;

        Assert.NotNull(sutA);
        Assert.Null(sutB);
        Assert.False(sutA.Equals(sutB));

        sutA.Delete();
    }

    [Fact]
    public void Same_temporary_folder_referenced_multiple_times_is_the_same()
    {
        var sutA = new TemporaryFolderInfo();
        TemporaryFolderInfo sutB = sutA;
        TemporaryFolderInfo sutC = sutB;

        Assert.True(sutA.Equals(sutB));
        Assert.True(sutA.Equals(sutC));

        sutA.Delete();
    }

    [Fact]
    public void Temporary_folder_has_expected_hash_code()
    {
        var sut = new TemporaryFolderInfo();
        int expectedHashCode = sut.FullName.GetHashCode();

        Assert.Equal(expectedHashCode, sut.GetHashCode());

        sut.Delete();
    }

    [Fact]
    public void Can_enumerate_all_subfolders()
    {
        var sut = new TemporaryFolderInfo();
        DirectoryInfo[] expectedFolders = CreateThreeTemporarySubfolders(sut);

        DirectoryInfo[] sutFolders = [.. sut.EnumerateFolders().OrderBy(folder => folder.FullName)];

        Assert.Equal(expectedFolders[0].FullName, sutFolders[0].FullName);
        Assert.Equal(expectedFolders[1].FullName, sutFolders[1].FullName);
        Assert.Equal(expectedFolders[2].FullName, sutFolders[2].FullName);

        sut.Delete(true);
    }

    [Fact]
    public void Can_enumerate_all_subfolders_using_search_pattern()
    {
        var sut = new TemporaryFolderInfo();
        DirectoryInfo[] expectedFolders = CreateThreeTemporarySubfolders(sut);

        DirectoryInfo[] sutFolders = [.. sut.EnumerateFolders("sub*").OrderBy(folder => folder.FullName)];

        Assert.Equal(expectedFolders[0].FullName, sutFolders[0].FullName);
        Assert.Equal(expectedFolders[1].FullName, sutFolders[1].FullName);
        Assert.Equal(expectedFolders[2].FullName, sutFolders[2].FullName);

        sut.Delete(true);
    }

    [Fact]
    public void Can_enumerate_all_subfolders_using_search_pattern_and_enumeration_options()
    {
        var sut = new TemporaryFolderInfo();
        DirectoryInfo[] expectedFolders = CreateThreeTemporarySubfolders(sut);

        EnumerationOptions options = new() { MatchCasing = MatchCasing.PlatformDefault };
        DirectoryInfo[] sutFolders = [.. sut.EnumerateFolders("sub*", options).OrderBy(folder => folder.FullName)];

        Assert.Equal(expectedFolders[0].FullName, sutFolders[0].FullName);
        Assert.Equal(expectedFolders[1].FullName, sutFolders[1].FullName);
        Assert.Equal(expectedFolders[2].FullName, sutFolders[2].FullName);

        sut.Delete(true);
    }

    [Fact]
    public void Can_enumerate_all_subfolders_using_search_pattern_and_search_option()
    {
        var sut = new TemporaryFolderInfo();
        DirectoryInfo[] expectedFolders = CreateThreeTemporarySubfolders(sut);

        DirectoryInfo[] sutFolders = [.. sut.EnumerateFolders("sub*", SearchOption.TopDirectoryOnly).OrderBy(folder => folder.FullName)];

        Assert.Equal(expectedFolders[0].FullName, sutFolders[0].FullName);
        Assert.Equal(expectedFolders[1].FullName, sutFolders[1].FullName);
        Assert.Equal(expectedFolders[2].FullName, sutFolders[2].FullName);

        sut.Delete(true);
    }

    [Fact]
    public void Can_enumerate_all_files()
    {
        var sut = new TemporaryFolderInfo();
        FileInfo[] expectedFiles = CreateThreeTemporaryFiles(sut);

        FileInfo[] sutFiles = [.. sut.EnumerateFiles().OrderBy(file => file.FullName)];

        Assert.Equal(expectedFiles[0].FullName, sutFiles[0].FullName);
        Assert.Equal(expectedFiles[1].FullName, sutFiles[1].FullName);
        Assert.Equal(expectedFiles[2].FullName, sutFiles[2].FullName);

        sut.Delete(true);
    }

    [Fact]
    public void Can_enumerate_all_files_using_search_pattern()
    {
        var sut = new TemporaryFolderInfo();
        FileInfo[] expectedFiles = CreateThreeTemporaryFiles(sut);

        FileInfo[] sutFiles = [.. sut.EnumerateFiles("file*").OrderBy(file => file.FullName)];

        Assert.Equal(expectedFiles[0].FullName, sutFiles[0].FullName);
        Assert.Equal(expectedFiles[1].FullName, sutFiles[1].FullName);
        Assert.Equal(expectedFiles[2].FullName, sutFiles[2].FullName);

        sut.Delete(true);
    }

    [Fact]
    public void Can_enumerate_all_files_using_search_pattern_and_enumeration_options()
    {
        var sut = new TemporaryFolderInfo();
        FileInfo[] expectedFiles = CreateThreeTemporaryFiles(sut);

        EnumerationOptions options = new() { MatchCasing = MatchCasing.PlatformDefault };
        FileInfo[] sutFiles = [.. sut.EnumerateFiles("file*", options).OrderBy(file => file.FullName)];

        Assert.Equal(expectedFiles[0].FullName, sutFiles[0].FullName);
        Assert.Equal(expectedFiles[1].FullName, sutFiles[1].FullName);
        Assert.Equal(expectedFiles[2].FullName, sutFiles[2].FullName);

        sut.Delete(true);
    }

    [Fact]
    public void Can_enumerate_all_files_using_search_pattern_and_search_option()
    {
        var sut = new TemporaryFolderInfo();
        FileInfo[] expectedFiles = CreateThreeTemporaryFiles(sut);

        FileInfo[] sutFiles = [.. sut.EnumerateFiles("file*", SearchOption.TopDirectoryOnly).OrderBy(file => file.FullName)];

        Assert.Equal(expectedFiles[0].FullName, sutFiles[0].FullName);
        Assert.Equal(expectedFiles[1].FullName, sutFiles[1].FullName);
        Assert.Equal(expectedFiles[2].FullName, sutFiles[2].FullName);

        sut.Delete(true);
    }

    internal static DirectoryInfo[] CreateThreeTemporarySubfolders(TemporaryFolderInfo temporaryFolderInfo)
    {
        const int Total = 3;
        var temporarySubfolders = new DirectoryInfo[Total];

        for (int index = 0; index < Total; index++)
        {
            temporarySubfolders[index] = temporaryFolderInfo.CreateSubfolder($"subfolder-{(index + 1):000}");
        }

        return temporarySubfolders;
    }

    internal static FileInfo[] CreateThreeTemporaryFiles(TemporaryFolderInfo temporaryFolderInfo)
    {
        const int Total = 3;
        var temporaryFiles = new FileInfo[Total];

        for (int index = 0; index < Total; index++)
        {
            temporaryFiles[index] = new FileInfo(
                Path.Combine(temporaryFolderInfo.FullName, $"file-{(index + 1):000}"));

            using (StreamWriter fileWriter = temporaryFiles[index].CreateText())
            {
                fileWriter.WriteLine($"This is file number #{(index + 1)}");
            }
        }

        return temporaryFiles;
    }
}
