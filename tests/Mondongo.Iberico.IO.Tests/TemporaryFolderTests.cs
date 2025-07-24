// ---------------------------------------------------------------------------------------------------------------------
// <copyright file="TemporaryFolderTests.cs" company="OscarNET-SOFTware">
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
using System.Text;

using Mondongo.Iberico.Extensions;
using Mondongo.Iberico.IO.Resources;

namespace Mondongo.Iberico.IO;

public sealed class TemporaryFolderTests
{
    [Fact]
    public void Ctor_should_create_expected_temporary_folder()
    {
        using var sut = new TemporaryFolder();

        Assert.True(Directory.Exists(sut.FolderInfo.FullName));
        Assert.True(sut.FolderInfo.Exists);
    }

    [Fact]
    public void Dispose_should_delete_temporary_folder()
    {
        string sutTemporaryFolderFullPath;
        using (var sut = new TemporaryFolder())
        {
            sutTemporaryFolderFullPath = sut.FolderInfo.FullName;
            Assert.True(Directory.Exists(sutTemporaryFolderFullPath));
        }

        Assert.False(Directory.Exists(sutTemporaryFolderFullPath));
    }

    [Fact]
    public void Clear_should_clean_temporary_folder_as_expected()
    {
        using var sut = new TemporaryFolder();

        Assert.Empty(sut.FolderInfo.EnumerateFolders());
        Assert.Empty(sut.FolderInfo.EnumerateFiles());

        DirectoryInfo[] expectedFolders = TemporaryFolderInfoTests.CreateThreeTemporarySubfolders(sut.FolderInfo);
        FileInfo[] expectedFiles = TemporaryFolderInfoTests.CreateThreeTemporaryFiles(sut.FolderInfo);

        FileInfo[] sutFiles = [.. sut.FolderInfo.EnumerateFiles().OrderBy(file => file.FullName)];
        Assert.NotEmpty(sutFiles);
        Assert.True(expectedFiles[0].Exists);
        Assert.True(expectedFiles[1].Exists);
        Assert.True(expectedFiles[2].Exists);

        DirectoryInfo[] sutFolders = [.. sut.FolderInfo.EnumerateFolders().OrderBy(folder => folder.FullName)];
        Assert.NotEmpty(sutFolders);
        Assert.True(expectedFolders[0].Exists);
        Assert.True(expectedFolders[1].Exists);
        Assert.True(expectedFolders[2].Exists);

        sut.Clear();

        Assert.Empty(sut.FolderInfo.EnumerateFolders());
        Assert.Empty(sut.FolderInfo.EnumerateFiles());

        expectedFiles[0].Refresh();
        expectedFiles[1].Refresh();
        expectedFiles[2].Refresh();
        expectedFolders[0].Refresh();
        expectedFolders[1].Refresh();
        expectedFolders[2].Refresh();

        Assert.False(expectedFiles[0].Exists);
        Assert.False(expectedFiles[1].Exists);
        Assert.False(expectedFiles[2].Exists);

        Assert.False(expectedFolders[0].Exists);
        Assert.False(expectedFolders[1].Exists);
        Assert.False(expectedFolders[2].Exists);
    }

    [Fact]
    public void Clear_should_throw_expected_exception_when_instance_has_been_disposed()
    {
        var sut = new TemporaryFolder();
        string expectedError = IOResources.TemporaryFolderCouldNotBeClear.FormatMessage(
            sut.FolderInfo.FullName, IOResources.TemporaryFolderHasBeenDisposed);

        sut.Dispose();

        Assert.Equal(expectedError, Assert.Throws<InvalidOperationException>(sut.Clear).Message);
    }

    [Fact]
    public void Clear_should_throw_expected_exception_when_temporary_folder_has_already_been_delete()
    {
        using var sut = new TemporaryFolder();
        string expectedError = IOResources.TemporaryFolderCouldNotBeClear.FormatMessage(
            sut.FolderInfo.FullName, IOResources.TemporaryDirectoryHasAlreadyBeenDeleted);

        sut.FolderInfo.Delete();

        Assert.Equal(expectedError, Assert.Throws<InvalidOperationException>(sut.Clear).Message);
    }

    [Fact]
    public void CreateFile_should_create_expected_file()
    {
        using var sut = new TemporaryFolder();
        using FileStream sutFile = sut.CreateFile();
        using FileStream sutFileA = sut.CreateFile("file-001.tmp");
        using FileStream sutFileB = sut.CreateFile("file-002.tmp");
        sutFile.Write([65, 66]);
        sutFileA.Write([65]);
        sutFileB.Write([66]);
        sutFile.Flush(true);
        sutFileA.Flush(true);
        sutFileB.Flush(true);

        Assert.Equal(sut.FolderInfo.FullName, Path.GetDirectoryName(sutFile.Name));
        Assert.Equal(sut.FolderInfo.FullName, Path.GetDirectoryName(sutFileA.Name));
        Assert.Equal(sut.FolderInfo.FullName, Path.GetDirectoryName(sutFileB.Name));
    }

    [Fact]
    public void CreateFile_should_throw_expected_exception_when_instance_has_been_disposed()
    {
        var sut = new TemporaryFolder();
        string expectedError = IOResources.TemporaryFolderFileCouldNotBeCreate.FormatMessage(
            sut.FolderInfo.FullName, IOResources.TemporaryFolderHasBeenDisposed);

        sut.Dispose();

        Assert.Equal(expectedError, Assert.Throws<InvalidOperationException>(
            () => { using FileStream fileStream = sut.CreateFile(); }).Message);
    }

    [Fact]
    public void CreateFile_should_throw_expected_exception_when_temporary_folder_has_already_been_delete()
    {
        using var sut = new TemporaryFolder();
        string expectedError = IOResources.TemporaryFolderFileCouldNotBeCreate.FormatMessage(
            sut.FolderInfo.FullName, IOResources.TemporaryDirectoryHasAlreadyBeenDeleted);

        sut.FolderInfo.Delete();

        Assert.Equal(expectedError, Assert.Throws<InvalidOperationException>(
            () => { using FileStream fileStream = sut.CreateFile(); }).Message);
    }

    [Fact]
    public void CreateTextFile_should_create_expected_file()
    {
        using var sut = new TemporaryFolder();
        using StreamWriter sutFile = sut.CreateTextFile();
        using StreamWriter sutFileA = sut.CreateTextFile(fileName: "text-file-001.tmp");
        using StreamWriter sutFileB = sut.CreateTextFile(Encoding.Unicode, "text-file-002.tmp");
        using StreamWriter sutFileC = sut.CreateTextFile(Encoding.ASCII);
        sutFile.WriteLine("This is a text file # ABC");
        sutFileA.WriteLine("This is a text file # A");
        sutFileB.WriteLine("This is a text file # B");
        sutFileC.WriteLine("This is a text file # C");
        sutFile.Flush();
        sutFileA.Flush();
        sutFileB.Flush();
        sutFileC.Flush();

        Assert.Equal(sut.FolderInfo.FullName, Path.GetDirectoryName(((FileStream)sutFile.BaseStream).Name));
        Assert.Equal(sut.FolderInfo.FullName, Path.GetDirectoryName(((FileStream)sutFileA.BaseStream).Name));
        Assert.Equal(sut.FolderInfo.FullName, Path.GetDirectoryName(((FileStream)sutFileB.BaseStream).Name));
        Assert.Equal(sut.FolderInfo.FullName, Path.GetDirectoryName(((FileStream)sutFileC.BaseStream).Name));

        Assert.Equal(Encoding.UTF8.BodyName, sutFile.Encoding.BodyName);
        Assert.Equal(Encoding.UTF8.BodyName, sutFileA.Encoding.BodyName);
        Assert.Equal(Encoding.Unicode.BodyName, sutFileB.Encoding.BodyName);
        Assert.Equal(Encoding.ASCII.BodyName, sutFileC.Encoding.BodyName);
    }

    [Fact]
    public void CreateTextFile_should_throw_expected_exception_when_instance_has_been_disposed()
    {
        var sut = new TemporaryFolder();
        string expectedError = IOResources.TemporaryFolderFileCouldNotBeCreate.FormatMessage(
            sut.FolderInfo.FullName, IOResources.TemporaryFolderHasBeenDisposed);

        sut.Dispose();

        Assert.Equal(expectedError, Assert.Throws<InvalidOperationException>(
            () => { using StreamWriter streamWriter = sut.CreateTextFile(); }).Message);
    }

    [Fact]
    public void CreateTextFile_should_throw_expected_exception_when_temporary_folder_has_already_been_delete()
    {
        using var sut = new TemporaryFolder();
        string expectedError = IOResources.TemporaryFolderFileCouldNotBeCreate.FormatMessage(
            sut.FolderInfo.FullName, IOResources.TemporaryDirectoryHasAlreadyBeenDeleted);

        sut.FolderInfo.Delete();

        Assert.Equal(expectedError, Assert.Throws<InvalidOperationException>(
            () => { using StreamWriter streamWriter = sut.CreateTextFile(); }).Message);
    }

}
