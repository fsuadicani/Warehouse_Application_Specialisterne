function DataTable({
  columns,
  rows,
  getRowKey,
  renderActions,
  actionHeader = 'Actions',
  actionPosition = 'end',
  tableClassName = '',
}) {
  return (
    <table className={tableClassName}>
      <thead>
        <tr>
          {renderActions && actionPosition === 'start' && (
            <th className="row-actions-header">{actionHeader}</th>
          )}
          {columns.map((column) => (
            <th key={column.key} className={column.headerClassName}>
              {column.label}
            </th>
          ))}
          {renderActions && actionPosition === 'end' && (
            <th className="row-actions-header">{actionHeader}</th>
          )}
        </tr>
      </thead>
      <tbody>
        {rows.map((row, index) => {
          const actionCell = renderActions ? (
            <td className="row-actions">{renderActions(row, index)}</td>
          ) : null;

          return (
            <tr key={getRowKey(row, index)}>
              {renderActions && actionPosition === 'start' && actionCell}
              {columns.map((column) => (
                <td key={column.key} className={column.cellClassName}>
                  {column.render ? column.render(row, index) : row[column.key]}
                </td>
              ))}
              {renderActions && actionPosition === 'end' && actionCell}
            </tr>
          );
        })}
      </tbody>
    </table>
  );
}

export default DataTable;
