import DataTable from '../components/DataTable.jsx';
import '../css/ui.css';
import { transits } from '../testdata/tableTestData.js';

function TransitsPage() {
  const transitColumns = [
    { key: 'start', label: 'Start' },
    { key: 'destination', label: 'Destination' },
    { key: 'transitNumber', label: 'Transit Number' },
    { key: 'pickUpCode', label: 'Pick Up Code' },
    { key: 'gpsLocation', label: 'GPS Location' },
    { key: 'distributor', label: 'Distributor' },
    { key: 'status', label: 'Status' },
  ];

  return (
    <div>

      <div className="header-container">
        <h1>Transits</h1>
      </div>

      <div className="button-container">
      </div>


      <div className="tablecontainer">
        <DataTable
          columns={transitColumns}
          rows={transits}
          getRowKey={(transit) => transit.transitNumber}
        />
      </div>
    </div>
  );
}

export default TransitsPage;
