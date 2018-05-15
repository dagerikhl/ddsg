using System.Runtime.Serialization;

namespace DdSG {

    public enum AssetCategory {

        [EnumMember(Value = "Client")]
        Client,
        [EnumMember(Value = "File System")]
        FileSystem,
        [EnumMember(Value = "Server Side Function")]
        ServerSideFunction,
        [EnumMember(Value = "LDAP System")]
        LdapSystem,
        [EnumMember(Value = "Scripting Host")]
        ScriptingHost,
        [EnumMember(Value = "Session Management System")]
        SessionManagementSystem,
        [EnumMember(Value = "Error Handling System")]
        ErrorHandlingSystem,
        [EnumMember(Value = "Data Parser")]
        DataParser,
        [EnumMember(Value = "Database")]
        Database,
        [EnumMember(Value = "E-mail System")]
        EmailSystem,
        [EnumMember(Value = "Buffer")]
        Buffer,
        [EnumMember(Value = "Configuration File")]
        ConfigurationFile,
        [EnumMember(Value = "Command Interpreter")]
        CommandInterpreter,
        [EnumMember(Value = "Message")]
        Message,
        [EnumMember(Value = "Operating System")]
        OperatingSystem

    }

}
