import DataTable from '../components/DataTable.jsx';
import '../css/ui.css';

function TransitsPage() {
  const transits = [
    {
      start: 'Odense',
      destination: 'Kobenhavn',
      transitNumber: 'TR-2001',
      pickUpCode: 'PU-3819',
      gpsLocation: '55.4038, 10.4024',
      distributor: 'PostNord',
      status: 'In Transit',
    },
    {
      start: 'Aarhus',
      destination: 'Odense',
      transitNumber: 'TR-2002',
      pickUpCode: 'PU-4821',
      gpsLocation: '56.1629, 10.2039',
      distributor: 'GLS',
      status: 'Ready for Pickup',
    },
    {
      start: 'Kobenhavn',
      destination: 'Aalborg',
      transitNumber: 'TR-2003',
      pickUpCode: 'PU-5934',
      gpsLocation: '55.6761, 12.5683',
      distributor: 'DHL',
      status: 'Delivered',
    },
  ];
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
