using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;

public class NaiveBayesTable
{
    public DataTable dataTable;

    public NaiveBayesTable() {
        dataTable = new DataTable();
        dataTable.Columns.Add("Moves", typeof(string));
        dataTable.Columns.Add("Won", typeof(bool));
        dataTable.Columns.Add("Strength", typeof(string));
    }

    public void AddRecord(PlayerClassifier record) {
        DataRow dataRow = dataTable.NewRow();
        dataRow["Moves"] = record.moves;
        dataRow["Won"] = record.won;
        dataRow["Strength"] = record.handStrength;
        dataTable.Rows.Add(dataRow);
    }

    public DataRow GetRecord(int index) {
        if (index > dataTable.Rows.Count)
            return null;
        return dataTable.Rows[index];
    }

    public List<DataRow> GetRecordsMatchingMovesFromPreflop(string moves) {
        string expression = "Preflop LIKE '*" + moves + "*'";
        List<DataRow> rows = new List<DataRow>(dataTable.Select(expression));
        return rows;
    }

    public List<DataRow> GetRecordsMatchingMovesFromFlop(string moves) {
        string expression = "Flop LIKE '*" + moves + "*'";
        List<DataRow> rows = new List<DataRow>(dataTable.Select(expression));
        return rows;
    }

    public List<DataRow> GetRecordsMatchingMovesFromRiver(string moves) {
        string expression = "River LIKE '*" + moves + "*'";
        List<DataRow> rows = new List<DataRow>(dataTable.Select(expression));
        return rows;
    }

    public List<DataRow> GetRecordsMatchingMovesFromTurn(string moves) {
        string expression = "Turn LIKE '*" + moves + "*'";
        List<DataRow> rows = new List<DataRow>(dataTable.Select(expression));
        return rows;
    }
}
