import '../css/warehouse.css';

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

  return (
    <div className="content">
      <h1>Transits</h1>

      <div className="tablecontainer">
        <table>
          <thead>
            <tr>
              <th>Start</th>
              <th>Destination</th>
              <th>Transit Number</th>
              <th>Pick Up Code</th>
              <th>GPS Location</th>
              <th>Distributor</th>
              <th>Status</th>
            </tr>
          </thead>
          <tbody>
            {transits.map((transit) => (
              <tr key={transit.transitNumber}>
                <td>{transit.start}</td>
                <td>{transit.destination}</td>
                <td>{transit.transitNumber}</td>
                <td>{transit.pickUpCode}</td>
                <td>{transit.gpsLocation}</td>
                <td>{transit.distributor}</td>
                <td>{transit.status}</td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}

export default TransitsPage;
