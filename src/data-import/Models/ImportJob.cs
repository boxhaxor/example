namespace data_import.Models;


public enum ImportStatus {
    Imported,
    StartingProcessing,
    FinishedProcessing
}

public class ImportJob {
    public int JobId {get;set;}
    public ImportStatus Status {get;set;}
    public string ImportPath {get;set;}
}