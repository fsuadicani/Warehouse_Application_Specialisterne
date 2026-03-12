import { useEffect, useRef } from 'react';

const Graphs = () => {
  const containerRef = useRef(null);
  const chartRef = useRef(null);

  useEffect(() => {
    // Wait a tick to ensure DOM is ready and FusionCharts is available
    const timer = setTimeout(() => {
      if (!containerRef.current) return;
      
      const myDataSource = {
        chart: {
          caption: "Top 10 iOS Apps - July 2017",
          subCaption: "Downloads (In Millions)",
          "canvasBgAlpha": "0",
          "bgColor": "#ffffff",
          "bgAlpha": "70",
          "baseFont": "Roboto",
          "baseFontSize": "14",
          "showAlternateVGridColor": "1",
          "alternateVGridAlpha": "5",
          "labelFontSize": "15",
          "captionFontSize": "20",
          "subCaptionFontSize": "16",
          "toolTipColor": "#000000",
          "toolTipBgColor": "#ffffff",
          "toolTipAlpha": "90",
          "captionFontBold": "0",
          "subCaptionFontBold": "0",
          "paletteColors": "#8E24AA",
          "valueFontSize": "13",
          "valueFontBold": "0",
          "animation": "0",
          "divLineAlpha": "15",
          "divLineDashed": "0",
          "plotFillAlpha": "90",
          theme: "ocean"
        },
        "data": [
          { "label": "Sarahah", "value": "3880000" },
          { "label": "Messenger", "value": "2570000" },
          { "label": "Snake vs Block", "value": "2420000" },
          { "label": "Facebook", "value": "2140000" },
          { "label": "Amazon", "value": "1830000" },
          { "label": "Spotify Music", "value": "1540000" },
          { "label": "Netflix", "value": "1530000" },
          { "label": "Word Connect", "value": "1440000" },
          { "label": "Pandora", "value": "1300000" },
          { "label": "WhatsApp Messenger", "value": "1210000" }
        ]
      };

      const barChartConfigs = {
        id: "bar-chart",
        renderAt: "chart-container",
        type: "bar2d",
        width: "100%",
        height: 400,
        dataFormat: "json",
        dataSource: myDataSource
      };

      if (window.FusionCharts) {
        if (chartRef.current) {
          chartRef.current.dispose();
          chartRef.current = null;
        }

        chartRef.current = new window.FusionCharts(barChartConfigs);
        chartRef.current.render();
      } else {
        console.warn('FusionCharts not available');
      }
    }, 100); // 100ms delay to ensure scripts load

    return () => {
      clearTimeout(timer);

      if (chartRef.current) {
        chartRef.current.dispose();
        chartRef.current = null;
      }
    };
  }, []);

  return <div ref={containerRef} id="chart-container" style={{ width: '100%', height: '400px', minHeight: '400px' }}></div>;
};

export default Graphs;
