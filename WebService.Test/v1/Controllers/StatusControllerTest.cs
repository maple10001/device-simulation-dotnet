﻿// Copyright (c) Microsoft. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.IoTSolutions.DeviceSimulation.Services;
using Microsoft.Azure.IoTSolutions.DeviceSimulation.Services.Concurrency;
using Microsoft.Azure.IoTSolutions.DeviceSimulation.Services.Diagnostics;
using Microsoft.Azure.IoTSolutions.DeviceSimulation.Services.IotHub;
using Microsoft.Azure.IoTSolutions.DeviceSimulation.Services.Runtime;
using Microsoft.Azure.IoTSolutions.DeviceSimulation.Services.StorageAdapter;
using Microsoft.Azure.IoTSolutions.DeviceSimulation.SimulationAgent;
using Moq;
using WebService.Test.helpers;
using Xunit;
using Xunit.Abstractions;
using SimulationModel = Microsoft.Azure.IoTSolutions.DeviceSimulation.Services.Models.Simulation;
using StatusController = Microsoft.Azure.IoTSolutions.DeviceSimulation.WebService.v1.Controllers.StatusController;

namespace WebService.Test.v1.Controllers
{
    public class StatusControllerTest
    {
        private const string SIMULATION_ID = "1";

        private readonly Mock<IPreprovisionedIotHub> preprovisionedIotHub;
        private readonly Mock<IStorageAdapterClient> storage;
        private readonly Mock<ISimulations> simulations;
        private readonly Mock<ILogger> logger;
        private readonly Mock<IServicesConfig> servicesConfig;
        private readonly Mock<IDeploymentConfig> deploymentConfig;
        private readonly Mock<IIotHubConnectionStringManager> connectionStringManager;
        private readonly Mock<ISimulationRunner> simulationRunner;
        private readonly Mock<IRateLimiting> rateReporter;
        private readonly StatusController target;

        public StatusControllerTest(ITestOutputHelper log)
        {
            this.preprovisionedIotHub = new Mock<IPreprovisionedIotHub>();
            this.storage = new Mock<IStorageAdapterClient>();
            this.simulations = new Mock<ISimulations>();
            this.logger = new Mock<ILogger>();
            this.servicesConfig = new Mock<IServicesConfig>();
            this.deploymentConfig = new Mock<IDeploymentConfig>();
            this.connectionStringManager = new Mock<IIotHubConnectionStringManager>();
            this.simulationRunner = new Mock<ISimulationRunner>();
            this.rateReporter = new Mock<IRateLimiting>();

            this.target = new StatusController(
                this.preprovisionedIotHub.Object,
                this.storage.Object,
                this.simulations.Object,
                this.logger.Object,
                this.servicesConfig.Object,
                this.deploymentConfig.Object,
                this.connectionStringManager.Object,
                this.simulationRunner.Object,
                this.rateReporter.Object);
        }

        [Fact, Trait(Constants.TYPE, Constants.UNIT_TEST)]
        public async Task ItReturnsTheNumberOfActiveDevices()
        {
            // Arrange
<<<<<<< HEAD
            const int ACTIVE_DEVICES_COUNT = 5;
            this.SetupSimulationForRunner();
=======
            const int ACTIVE_DEVICE_COUNT = 5;
            const int FAILED_MESSAGE_COUNT = 1;
            const int TOTAL_MESSAGE_COUNT = 1;
            const double MESSAGE_THROUGHPUT = 15.5556;
            var updatedValue = new ValueApiModel { ETag = "newETag" };
            const string IOTHUB_CONNECTION_STRING = "hostname=hub-1;sharedaccesskeyname=hubowner;sharedaccesskey=fakekey";
            Tuple<bool, string> CONNECTIONSTRING = new Tuple<bool, string>(true, IOTHUB_CONNECTION_STRING);

            // Simulation status
            SetupSimulationForRunner();
>>>>>>> Add telemetry metrics support

            // Storage status
            this.storage.Setup(x => x.UpdateAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(updatedValue);

            // Preprovisioned hub status
            this.preprovisionedIotHub
                .Setup(x => x.PingRegistryAsync())
                .ReturnsAsync(CONNECTIONSTRING);

            this.servicesConfig
               .Setup(x => x.IoTHubConnString)
               .Returns(IOTHUB_CONNECTION_STRING);

            this.connectionStringManager
                .Setup(x => x.GetIotHubConnectionString())
                .Returns(IOTHUB_CONNECTION_STRING);

            // Active devices status
            this.simulationRunner
                .Setup(x => x.GetActiveDevicesCount())
                .Returns(ACTIVE_DEVICES_COUNT);

            // Total telemetry messages count
            this.simulationRunner
                .Setup(x => x.GetTotalMessagesCount())
                .Returns(TOTAL_MESSAGE_COUNT);

            // Failed telemetry messages count
            this.simulationRunner
                .Setup(x => x.GetFailedMessagesCount())
                .Returns(FAILED_MESSAGE_COUNT);

            // Telemetry messages thoughput
            this.rateReporter
                .Setup(x => x.GetThroughputForMessages())
                .Returns(MESSAGE_THROUGHPUT);

            // Act
            var result = await this.target.Get();

            // Assert
<<<<<<< HEAD
            Assert.Equal(ACTIVE_DEVICES_COUNT.ToString(), result.Properties["ActiveDevicesCount"]);
=======
            Assert.Equal(8, result.Properties.Count);
            Assert.Equal("true", result.Properties["SimulationRunning"]);
            Assert.Equal("true", result.Properties["PreprovisionedIoTHub"]);
            Assert.Contains("https://portal.azure.com/", result.Properties["PreprovisionedIoTHubMetricsUrl"]);
            Assert.Equal("true", result.Properties["PreprovisionedIoTHubInUse"]); 
            Assert.Equal(ACTIVE_DEVICE_COUNT.ToString(), result.Properties["ActiveDeviceCount"]);
            Assert.Equal(MESSAGE_THROUGHPUT.ToString("F"), result.Properties["MessagesPerSecond"]);
            Assert.Equal(ACTIVE_DEVICE_COUNT.ToString(), result.Properties["ActiveDeviceCount"]);
            Assert.Equal(FAILED_MESSAGE_COUNT.ToString(), result.Properties["FailedMessagesCount"]);
>>>>>>> Add telemetry metrics support
        }

        private void SetupSimulationForRunner()
        {
            var simulation = new SimulationModel
            {
                Id = SIMULATION_ID,
                Created = DateTimeOffset.UtcNow.Subtract(TimeSpan.FromDays(10)),
                Modified = DateTimeOffset.UtcNow.Subtract(TimeSpan.FromDays(10)),
                ETag = "ETag0",
                Enabled = true,
                Version = 1
            };

            var simulations = new List<SimulationModel>
            {
                simulation
            };

            this.simulations
                .Setup(x => x.GetListAsync())
                .ReturnsAsync(simulations);
        }
    }
}
